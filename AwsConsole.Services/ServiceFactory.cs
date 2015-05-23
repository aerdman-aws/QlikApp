using AwsConsole.Services.Deploy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsConsole.Services
{
    public class ServiceFactory
    {
        /// <summary>
        /// Returns a reference to the deployment service that can been used to access and deploy EC2 instances
        /// The factory will determine which implementation of the service to return
        /// </summary>
        /// <returns>A reference to the deployment service</returns>
        public static IDeploymentService GetDeploymentService() {
            return new DeploymentService();
        }
    }
}
