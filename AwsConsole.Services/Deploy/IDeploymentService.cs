using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsConsole.Services.Deploy
{
    public interface IDeploymentService
    {
        SecurityGroup GetSecurityGroup();
        SecurityGroup CreateSecurityGroup();
        
        Instance GetInstanceByName(string instanceName);
        Instance GetInstanceById(string instanceId);
        Instance CreateInstance(SecurityGroup securityGroup);
    }
}
