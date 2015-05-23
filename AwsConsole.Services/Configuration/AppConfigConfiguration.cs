using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsConsole.Services.Configuration
{
    public class AppConfigConfiguration : IConfiguration
    {
        private const string AWSRegionKey = "AWSRegion";

        private const string InstanceNameKey = "QlikAppInstanceName";
        private const string InstanceTypeKey = "QlikAppInstanceType";
        private const string InstanceImageIdKey = "QlikAppInstanceImageId";
        private const string InstanceSubnetIdKey = "QlikAppInstanceSubnetId";
        private const string InstanceKeyPairNameKey = "QlikAppInstanceKeyPairName";

        private const string VpcIdKey = "QlikAppVpcId";
        private const string SecurityGroupNameKey = "QlikAppSecurityGroupName";
        private const string SecurityGroupDescriptionKey = "QlikAppSecurityGroupDescription";
        private const string SecurityGroupIpRangesKey = "QlikAppSecurityGroupIpRanges";
        private const string SecurityGroupIpPermissionsKey = "QlikAppSecurityGroupIpPermissions";

        public AppConfigConfiguration()
        {

        }

        public string AWSRegion
        {
            get
            {
                return ConfigurationManager.AppSettings[AWSRegionKey];
            }
        }

        public string InstanceName
        {
            get
            {
                return ConfigurationManager.AppSettings[InstanceNameKey];
            }
        }

        public string InstanceType
        {
            get
            {
                return ConfigurationManager.AppSettings[InstanceTypeKey];
            }
        }

        public string InstanceImageId
        {
            get
            {
                return ConfigurationManager.AppSettings[InstanceImageIdKey];
            }
        }

        public string InstanceSubnetId
        {
            get
            {
                return ConfigurationManager.AppSettings[InstanceSubnetIdKey];
            }
        }

        public string InstanceKeyPairName
        {
            get
            {
                return ConfigurationManager.AppSettings[InstanceKeyPairNameKey];
            }
        }

        public string VpcId
        {
            get
            {
                return ConfigurationManager.AppSettings[VpcIdKey];
            }
        }

        public string SecurityGroupName
        {
            get
            {
                return ConfigurationManager.AppSettings[SecurityGroupNameKey];
            }
        }

        public string SecurityGroupDescription
        {
            get
            {
                return ConfigurationManager.AppSettings[SecurityGroupDescriptionKey];
            }
        }

        public string SecurityGroupIpRanges
        {
            get
            {
                return ConfigurationManager.AppSettings[SecurityGroupIpRangesKey];
            }
        }

        public string SecurityGroupIpPermissions
        {
            get
            {
                return ConfigurationManager.AppSettings[SecurityGroupIpPermissionsKey];
            }
        }
    }
}
