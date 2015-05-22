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
        public static IDeploymentService GetDeploymentService() {
            return new DeploymentService();
        }
    }
}
