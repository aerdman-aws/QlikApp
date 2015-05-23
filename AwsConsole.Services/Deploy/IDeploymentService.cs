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
        /// <summary>
        /// Get the security group that should be used by the instance defined in the deployment configuration
        /// </summary>
        /// <returns>The security group if it exists, otherwise null</returns>
        SecurityGroup GetSecurityGroup();
        
        /// <summary>
        /// Creates the security group that should be used by the instance
        /// </summary>
        /// <returns>The security group to be used by the instance</returns>
        SecurityGroup CreateSecurityGroup();
        
        /// <summary>
        /// Gets the EC2 instance defined in the deployment configuration
        /// </summary>
        /// <returns>The EC2 instance</returns>
        Instance GetInstance();

        /// <summary>
        /// Creates the EC2 instance that is defined in the deployment configuration
        /// </summary>
        /// <param name="securityGroup">The security group that the instance should use</param>
        /// <returns>The EC2 instance</returns>
        Instance CreateInstance(SecurityGroup securityGroup);

        /// <summary>
        /// Waits until the given instance is in the "Running" state
        /// </summary>
        /// <param name="instance">The instance to check the state of</param>
        /// <returns>The instance that is now in the "Running" state</returns>
        Instance WaitForInstanceToStart(Instance instance);
    }
}
