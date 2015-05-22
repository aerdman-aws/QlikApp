using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsConsole.Services.Deploy
{
    class DeploymentService : IDeploymentService
    {
        public DeploymentService()
        {
            EC2Client = AWSClientFactory.CreateAmazonEC2Client();
        }

        protected IAmazonEC2 EC2Client { get; set; }

        public void GetInstances()
        {
            DescribeInstancesRequest ec2Request = new DescribeInstancesRequest();

            DescribeInstancesResponse ec2Response = EC2Client.DescribeInstances(ec2Request);
            int numInstances = 0;
            numInstances = ec2Response.Reservations.Count;
            Console.WriteLine(string.Format("You have {0} Amazon EC2 instance(s) running in the {1} region.", numInstances, ConfigurationManager.AppSettings["AWSRegion"]));

            //TODO: ensure instance exists by name
            //TODO: ensure instance is running
        }

        public SecurityGroup GetSecurityGroup()
        {
            var mySGId = "sg-010d1464"; //TODO: move to configuration
            SecurityGroup mySG = null;

            var dsgRequest = new DescribeSecurityGroupsRequest();
            var dsgResponse = EC2Client.DescribeSecurityGroups(dsgRequest);
            List<SecurityGroup> mySGs = dsgResponse.SecurityGroups;

            foreach (SecurityGroup sg in mySGs)
            {
                Console.WriteLine(String.Format("Existing security group: {0} ({1}) - {2}", sg.GroupName, sg.GroupId, sg.VpcId));
                if (sg.GroupId == mySGId)
                {
                    mySG = sg;
                }
            }

            return mySG;
        }

        public SecurityGroup CreateSecurityGroup()
        {
            //TODO: store in configuration object that is injected, can be saved/loaded from DB or config file
            const string vpcId = "vpc-96ab29f3";
            const string securityGroupName = "qlik-demo-security-group";

            var newSGRequest = new CreateSecurityGroupRequest()
            {
                GroupName = securityGroupName,
                Description = "Security group for Qlik Demo",
                VpcId = vpcId
            };
            var csgResponse = EC2Client.CreateSecurityGroup(newSGRequest);
            Console.WriteLine();
            Console.WriteLine("New security group: " + csgResponse.GroupId);

            List<string> Groups = new List<string>() { csgResponse.GroupId };
            var newSgRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
            var newSgResponse = EC2Client.DescribeSecurityGroups(newSgRequest);

            //TODO: error handling
            var mySG = newSgResponse.SecurityGroups[0];
            
            List<string> ranges = new List<string>() { "0.0.0.0/0" };
            var rdpPermission = new IpPermission()
            {
                IpProtocol = "tcp",
                FromPort = 3389,
                ToPort = 3389,
                IpRanges = ranges
            };
            var httpPermission = new IpPermission()
            {
                IpProtocol = "tcp",
                FromPort = 80,
                ToPort = 80,
                IpRanges = ranges
            };

            var ingressRequest = new AuthorizeSecurityGroupIngressRequest();
            ingressRequest.GroupId = mySG.GroupId;
            ingressRequest.IpPermissions.Add(rdpPermission);
            ingressRequest.IpPermissions.Add(httpPermission);

            var ingressResponse = EC2Client.AuthorizeSecurityGroupIngress(ingressRequest);
            Console.WriteLine("Add permissions to security group: " + ingressResponse.HttpStatusCode); //TODO: check status code for error

            return mySG;
        }

        public void CreateInstance(SecurityGroup mySG)
        {
            //TODO: store in configuration object that is injected, can be saved/loaded from DB or config file
            const string subnetID = "subnet-4e54ce2b";
            const string amiID = "ami-c5bf8df5"; //custom image
            //"ami-63e89b53"; //Windows 2012 + IIS8 "ami-95d3f9a5"; //Windows 2012 + SQL
            const string keyPairName = "Ariel1";

            List<string> groups = new List<string>() { mySG.GroupId };
            var eni = new InstanceNetworkInterfaceSpecification()
            {
                DeviceIndex = 0,
                SubnetId = subnetID,
                Groups = groups,
                AssociatePublicIpAddress = true
            };
            List<InstanceNetworkInterfaceSpecification> enis = new List<InstanceNetworkInterfaceSpecification>() { eni };

            var launchRequest = new RunInstancesRequest()
            {
                ImageId = amiID,
                InstanceType = "t2.micro",
                MinCount = 1,
                MaxCount = 1,
                KeyName = keyPairName,
                NetworkInterfaces = enis,
            };

            var launchResponse = EC2Client.RunInstances(launchRequest);
            List<Instance> instances = launchResponse.Reservation.Instances;
            List<String> instanceIds = new List<string>();
            foreach (Instance item in instances)
            {
                instanceIds.Add(item.InstanceId);
                Console.WriteLine();
                Console.WriteLine("New instance: " + item.InstanceId);
            }

            var createTagsRequest = new CreateTagsRequest()
            {
                Resources = instanceIds,
                Tags = new[] { new Tag("Name", "QlikDemo") }.ToList()
            };
            var createTagsResponse = EC2Client.CreateTags(createTagsRequest);

            while (true)
            {
                var instancesRequest = new DescribeInstancesRequest();
                instancesRequest.InstanceIds = instanceIds;

                var statusResponse = EC2Client.DescribeInstances(instancesRequest);
                var runningInstance = statusResponse.Reservations[0].Instances[0];

                if (runningInstance.State.Code == 16)
                {
                    Console.WriteLine("Instance is now available at: " + runningInstance.PublicDnsName);
                    break;
                }
                Console.WriteLine(String.Format("Instance status: {0} ({1})", runningInstance.State.Name, runningInstance.State.Code));
                System.Threading.Thread.Sleep(10 * 1000);
            }
        }
    }
}
