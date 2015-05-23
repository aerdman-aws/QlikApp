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
        /// <summary>
        /// Returns a reference to the deployment configuration
        /// The factory will determine which implementation of the configuration to return
        /// </summary>
        /// <returns>A reference to the deployment configuration</returns>
        public static IConfiguration GetDeploymentConfiguration()
        {
            return new AppConfigConfiguration();
        }
    }
}
