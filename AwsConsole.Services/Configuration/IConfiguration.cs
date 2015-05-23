using System;
namespace AwsConsole.Services.Configuration
{
    /// <summary>
    /// Contains all the information required to deploy the EC2 instance
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// The region where the instance should be deployed
        /// </summary>
        string AWSRegion { get; }

        /// <summary>
        /// The name of the instance
        /// </summary>
        string InstanceName { get; }

        /// <summary>
        /// The type of instance to deploy
        /// </summary>
        string InstanceType { get; }

        /// <summary>
        /// The Id of the image (AMI) to deploy the instance with
        /// </summary>
        string InstanceImageId { get; }

        /// <summary>
        /// The key-pair name the deployed instance should use
        /// </summary>
        string InstanceKeyPairName { get; }

        /// <summary>
        /// The Id of the subnet to use with the deployed instance
        /// </summary>
        string InstanceSubnetId { get; }

        /// <summary>
        /// The name of the security group used by the instance
        /// </summary>
        string SecurityGroupName { get; }

        /// <summary>
        /// The description of the security group
        /// </summary>
        string SecurityGroupDescription { get; }

        /// <summary>
        /// The IP ranges to apply the security group's permissions
        /// </summary>
        string SecurityGroupIpRanges { get; }

        /// <summary>
        /// The permissions to apply to the security group
        /// </summary>
        string SecurityGroupIpPermissions { get; }

        /// <summary>
        /// The ID of the VPC to use for the security group
        /// </summary>
        string VpcId { get; }
    }
}
