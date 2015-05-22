using Amazon.EC2.Model;
using AwsConsole.Services.Deploy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsConsole.Services.Commands
{
    public class GetSecurityGroupCommand : AwsCommand<SecurityGroup>
    {
        public GetSecurityGroupCommand()
        {
            DeploymentService = ServiceFactory.GetDeploymentService();
        }

        protected IDeploymentService DeploymentService { get; set; }

        public override SecurityGroup Execute()
        {
            return DeploymentService.GetSecurityGroup();
        }
    }
}
