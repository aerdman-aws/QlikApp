using Amazon.EC2;
using AwsConsole.Services;
using AwsConsole.Services.Deploy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsConsole
{
    class AwsDeployer
    {
        /// <summary>
        /// Creates the default AwsDeployer with the default deployment service
        /// </summary>
        public AwsDeployer()
        {
            DeploymentService = ServiceFactory.GetDeploymentService();
        }

        /// <summary>
        /// Creates an AwsDeployer with the given deployment service.
        /// Useful for unit testing
        /// </summary>
        /// <param name="deploymentService">The deployment service to use</param>
        public AwsDeployer(IDeploymentService deploymentService)
        {
            DeploymentService = deploymentService;
        }

        protected IDeploymentService DeploymentService { get; set; }

        public void DeployQlikAppInstance()
        {
            try
            {
                //attempt to get the EC2 instance
                var instance = DeploymentService.GetInstance();

                //check to see if the instance exists
                if (instance == null) //instance doesn't exist, will need to create it
                {
                    Console.WriteLine("Instance does not exist. Creating...");

                    //attempt to get the security group
                    var securityGroup = DeploymentService.GetSecurityGroup();
                    
                    //check to see if the security group exists
                    if (securityGroup == null)
                    {
                        //security group doesn't exist, create it
                        securityGroup = DeploymentService.CreateSecurityGroup();
                        if (securityGroup == null)
                        {
                            throw new Exception("Failed to create security group");
                        }
                    }

                    //create the instance using the security group
                    instance = DeploymentService.CreateInstance(securityGroup);
                    if (instance == null)
                    {
                        throw new Exception("Failed to create instance");
                    }
                }
                else //instance already exists
                {
                    Console.WriteLine("Instance already exists");
                }

                //make sure the instance is running
                Console.WriteLine("Ensuring instance is running...");
                instance = DeploymentService.WaitForInstanceToStart(instance);
                if (instance == null)
                {
                    throw new Exception("Instance failed to start");
                }

                Console.WriteLine("Instance is running!");
                Console.WriteLine();
                Console.WriteLine(String.Format("QlikApp can be accessed available at: {0}/qlikapp ", instance.PublicDnsName));
            }
            catch (AmazonEC2Exception ex)
            {
                if (ex.ErrorCode != null && ex.ErrorCode.Equals("AuthFailure"))
                {
                    Console.WriteLine("The account you are using is not signed up for Amazon EC2.");
                    Console.WriteLine("You can sign up for Amazon EC2 at http://aws.amazon.com/ec2");
                }
                else
                {
                    Console.WriteLine("Caught Exception: " + ex.Message);
                    Console.WriteLine("Response Status Code: " + ex.StatusCode);
                    Console.WriteLine("Error Code: " + ex.ErrorCode);
                    Console.WriteLine("Error Type: " + ex.ErrorType);
                    Console.WriteLine("Request ID: " + ex.RequestId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to deploy EC2 instance:");
                Console.WriteLine(ex);
            }
        }
    }
}
