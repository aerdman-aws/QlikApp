using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsConsole.Services.Commands
{
    public class GetOrCreateSecurityGroupCommand : AwsCommand<SecurityGroup>
    {
        public override SecurityGroup Execute()
        {
            var getSecurityGroupCommand = new GetSecurityGroupCommand();
            
            var securityGroup = getSecurityGroupCommand.Execute();
            if (securityGroup == null)
            {
                var createSecurityGroupCommand = new CreateSecurityGroupCommand();
                securityGroup = createSecurityGroupCommand.Execute();
            }

            return securityGroup;
        }
    }
}
