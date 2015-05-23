using System;
namespace AwsConsole.Services.Configuration
{
    public interface IConfiguration
    {
        string AWSRegion { get; }
        string InstanceImageId { get; }
        string InstanceKeyPairName { get; }
        string InstanceName { get; }
        string InstanceSubnetId { get; }
        string InstanceType { get; }
        string SecurityGroupDescription { get; }
        string SecurityGroupId { get; }
        string SecurityGroupIpPermissions { get; }
        string SecurityGroupIpRanges { get; }
        string SecurityGroupName { get; }
        string VpcId { get; }
    }
}
