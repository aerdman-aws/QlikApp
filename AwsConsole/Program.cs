using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using System.Threading;

namespace AwsConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            GetServiceOutput();
            Console.Read();
        }

        public static void GetServiceOutput()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Welcome to the AWS .NET SDK!");
            Console.WriteLine("===========================================");

            // Print the number of Amazon EC2 instances.
            IAmazonEC2 ec2Client = AWSClientFactory.CreateAmazonEC2Client();
            DescribeInstancesRequest ec2Request = new DescribeInstancesRequest();

            try
            {
                DescribeInstancesResponse ec2Response = ec2Client.DescribeInstances(ec2Request);
                int numInstances = 0;
                numInstances = ec2Response.Reservations.Count;
                Console.WriteLine(string.Format("You have {0} Amazon EC2 instance(s) running in the {1} region.",
                                           numInstances, ConfigurationManager.AppSettings["AWSRegion"]));
            }
            catch (AmazonEC2Exception ex)
            {
                if (ex.ErrorCode != null && ex.ErrorCode.Equals("AuthFailure"))
                {
                    Console.WriteLine("The account you are using is not signed up for Amazon EC2.");
                    Console.WriteLine("You can sign up for Amazon EC2 at http://aws.amazon.com/ec2");
                }
                else
                {
                    Console.WriteLine("Caught Exception: " + ex.Message);
                    Console.WriteLine("Response Status Code: " + ex.StatusCode);
                    Console.WriteLine("Error Code: " + ex.ErrorCode);
                    Console.WriteLine("Error Type: " + ex.ErrorType);
                    Console.WriteLine("Request ID: " + ex.RequestId);
                }
            }

            Console.WriteLine();

            var mySGId = "sg-010d1464";
            SecurityGroup mySG = null;
            var dsgRequest = new DescribeSecurityGroupsRequest();
            var dsgResponse = ec2Client.DescribeSecurityGroups(dsgRequest);
            List<SecurityGroup> mySGs = dsgResponse.SecurityGroups;
            foreach (SecurityGroup sg in mySGs)
            {
                Console.WriteLine(String.Format("Existing security group: {0} ({1}) - {2}", sg.GroupName, sg.GroupId, sg.VpcId));
                if (sg.GroupId == mySGId)
                {
                    mySG = sg;
                }
            }

            if (mySG == null)
            {
                mySG = createSecurityGroup(ec2Client);
                //TODO: error handling
            }

            if (mySG == null)
            {
                throw new InvalidOperationException("Unable to get security group");
            }

            //createEC2Instance(ec2Client, mySG);
            //password: 9pJu=%)gkWS

            var instancesRequest = new DescribeInstancesRequest()
            {
                InstanceIds = new[] { "i-161c99e1" }.ToList()
            };
            var statusResponse = ec2Client.DescribeInstances(instancesRequest);
            var runningInstance = statusResponse.Reservations[0].Instances[0];

            Console.WriteLine(runningInstance.InstanceId);

            Console.WriteLine("Press any key to continue...");
        }

        private static SecurityGroup createSecurityGroup(IAmazonEC2 ec2Client)
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
            var csgResponse = ec2Client.CreateSecurityGroup(newSGRequest);
            Console.WriteLine();
            Console.WriteLine("New security group: " + csgResponse.GroupId);

            List<string> Groups = new List<string>() { csgResponse.GroupId };
            var newSgRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
            var newSgResponse = ec2Client.DescribeSecurityGroups(newSgRequest);
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

            var ingressResponse = ec2Client.AuthorizeSecurityGroupIngress(ingressRequest);
            Console.WriteLine("Add permissions to security group: " + ingressResponse.HttpStatusCode); //TODO: check status code for error

            return mySG;
        }

        private static void createEC2Instance(IAmazonEC2 ec2Client, SecurityGroup mySG)
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

            var launchResponse = ec2Client.RunInstances(launchRequest);
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
            var createTagsResponse = ec2Client.CreateTags(createTagsRequest);

            while (true)
            {
                var instancesRequest = new DescribeInstancesRequest();
                instancesRequest.InstanceIds = instanceIds;

                var statusResponse = ec2Client.DescribeInstances(instancesRequest);
                var runningInstance = statusResponse.Reservations[0].Instances[0];

                if (runningInstance.State.Code == 16)
                {
                    Console.WriteLine("Instance is now available at: " + runningInstance.PublicDnsName);
                    break;
                }
                Console.WriteLine(String.Format("Instance status: {0} ({1})", runningInstance.State.Name, runningInstance.State.Code));
                Thread.Sleep(10 * 1000);
            }
        }
    }
}