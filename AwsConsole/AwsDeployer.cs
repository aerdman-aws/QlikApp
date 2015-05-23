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
        public AwsDeployer()
        {
            DeploymentService = ServiceFactory.GetDeploymentService();
        }

        public AwsDeployer(IDeploymentService deploymentService)
        {
            DeploymentService = deploymentService;
        }

        protected IDeploymentService DeploymentService { get; set; }

        public void DeployQlikAppInstance()
        {
            try
            {
                var instance = DeploymentService.GetInstance();
                if (instance == null)
                {
                    Console.WriteLine("Instance does not exist. Creating...");

                    var securityGroup = DeploymentService.GetSecurityGroup();
                    if (securityGroup == null)
                    {
                        securityGroup = DeploymentService.CreateSecurityGroup();
                        if (securityGroup == null)
                        {
                            throw new Exception("Failed to create security group");
                        }
                    }

                    instance = DeploymentService.CreateInstance(securityGroup);
                    if (instance == null)
                    {
                        throw new Exception("Failed to create instance");
                    }
                }
                else
                {
                    Console.WriteLine("Instance already exists");
                }

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
