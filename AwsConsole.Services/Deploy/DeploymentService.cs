using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using AwsConsole.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsConsole.Services.Deploy
{
    class DeploymentService : IDeploymentService
    {
        public DeploymentService()
        {
            Configuration = ConfigurationFactory.GetDeploymentConfiguration();
            EC2Client = AWSClientFactory.CreateAmazonEC2Client();
        }

        public DeploymentService(IConfiguration configuration, IAmazonEC2 ec2Client)
        {
            Configuration = configuration;
            EC2Client = ec2Client;
        }

        protected IConfiguration Configuration { get; set; }
        protected IAmazonEC2 EC2Client { get; set; }
        
        public SecurityGroup GetSecurityGroup()
        {
            var securityGroupId = Configuration.SecurityGroupId;
            var dsgRequest = new DescribeSecurityGroupsRequest();
            //Filter by name?
            var dsgResponse = EC2Client.DescribeSecurityGroups(dsgRequest);
            return dsgResponse.SecurityGroups.FirstOrDefault(sg => sg.GroupId == securityGroupId);
        }

        public SecurityGroup CreateSecurityGroup()
        {
            //Setup a new security group
            var newSGRequest = new CreateSecurityGroupRequest()
            {
                GroupName = Configuration.SecurityGroupName,
                Description = Configuration.SecurityGroupDescription,
                VpcId = Configuration.VpcId
            };
            var csgResponse = EC2Client.CreateSecurityGroup(newSGRequest);
            Console.WriteLine("Created new security group: " + csgResponse.GroupId);

            //Get the new security group
            var newSgRequest = new DescribeSecurityGroupsRequest() { GroupIds = new[] { csgResponse.GroupId }.ToList() };
            var newSgResponse = EC2Client.DescribeSecurityGroups(newSgRequest);
            
            var securityGroup = newSgResponse.SecurityGroups[0];

            //Setup permissions for the security group
            var ipRanges = Configuration.SecurityGroupIpRanges.Split(',').ToList();
            var permissions = Configuration.SecurityGroupIpPermissions.Split(',');

            var ipPermissions = permissions.Select(p =>
            {
                var protocol = p.Substring(0, 3);
                var port = int.Parse(p.Substring(3));
                return new IpPermission()
                {
                    IpProtocol = protocol,
                    FromPort = port,
                    ToPort = port,
                    IpRanges = ipRanges
                };
            });

            //Set the permissions on the security group
            var ingressRequest = new AuthorizeSecurityGroupIngressRequest();
            ingressRequest.GroupId = securityGroup.GroupId;
            ingressRequest.IpPermissions = ipPermissions.ToList();

            var ingressResponse = EC2Client.AuthorizeSecurityGroupIngress(ingressRequest);
            Console.WriteLine("Added permissions to security group: " + ingressResponse.HttpStatusCode);

            return securityGroup;
        }

        public Instance GetInstanceByName(string instanceName)
        {
            DescribeInstancesRequest ec2Request = new DescribeInstancesRequest();

            DescribeInstancesResponse ec2Response = EC2Client.DescribeInstances(ec2Request);

            var instances =
                from reservation in ec2Response.Reservations //check each reservation
                where reservation.Instances != null //that has instances
                from instance in reservation.Instances //check each instance
                where instance.Tags != null //that has tags
                let nameTag = instance.Tags.FirstOrDefault(tag => tag.Key == "Name") //find the name tag
                where nameTag != null && nameTag.Value == instanceName //check the name tag to see if it matches our instanceName
                select instance; //return the instance

            return instances.FirstOrDefault(); //return the first instance that has a name that matches our instanceName
        }

        public Instance GetInstanceById(string instanceId)
        {
            var instancesRequest = new DescribeInstancesRequest();
            instancesRequest.InstanceIds = new[] { instanceId }.ToList();

            var statusResponse = EC2Client.DescribeInstances(instancesRequest);
            if (statusResponse.Reservations != null && statusResponse.Reservations.Any())
            {
                if (statusResponse.Reservations[0].Instances != null && statusResponse.Reservations[0].Instances.Any())
                {
                    return statusResponse.Reservations[0].Instances[0];
                }
            }

            return null;
        }

        public Instance CreateInstance(SecurityGroup securityGroup)
        {
            //Setup the network interface for the instance
            List<string> groups = new List<string>() { securityGroup.GroupId };
            var eni = new InstanceNetworkInterfaceSpecification()
            {
                DeviceIndex = 0,
                SubnetId = Configuration.InstanceSubnetId,
                Groups = groups,
                AssociatePublicIpAddress = true
            };
            List<InstanceNetworkInterfaceSpecification> enis = new List<InstanceNetworkInterfaceSpecification>() { eni };

            //Setup the request for the new instance
            var launchRequest = new RunInstancesRequest()
            {
                ImageId = Configuration.InstanceImageId,
                InstanceType = Configuration.InstanceType,
                MinCount = 1,
                MaxCount = 1,
                KeyName = Configuration.InstanceKeyPairName,
                NetworkInterfaces = enis,
            };

            //Request the new instance
            var launchResponse = EC2Client.RunInstances(launchRequest);
            var instance = launchResponse.Reservation.Instances.FirstOrDefault();
            if (instance == null)
            {
                throw new Exception("Failed to create instance: " + launchResponse.HttpStatusCode);
            }
            Console.WriteLine("New instance created: " + instance.InstanceId);

            //Create a Name tag for the new instance
            var createTagsRequest = new CreateTagsRequest()
            {
                Resources = new[] { instance.InstanceId }.ToList(),
                Tags = new[] { new Tag("Name", Configuration.InstanceName) }.ToList()
            };
            var createTagsResponse = EC2Client.CreateTags(createTagsRequest);

            return instance;
        }
    }
}
