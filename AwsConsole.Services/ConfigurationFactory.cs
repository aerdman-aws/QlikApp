using AwsConsole.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsConsole.Services
{
    public class ConfigurationFactory
    {
        public static IConfiguration GetDeploymentConfiguration()
        {
            return new AppConfigConfiguration();
        }
    }
}
