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
                var instanceName = Configuration.Instance.InstanceName;
                Console.WriteLine(String.Format("Checking for the '{0}' Amazon EC2 instance running in the {1} region.", instanceName, Configuration.Instance.AWSRegion));

                var instance = DeploymentService.GetInstanceByName(instanceName);
                if (instance == null)
                {
                    Console.WriteLine(String.Format("Instance does not exist. Creating...", instanceName));

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
                    Console.WriteLine(String.Format("Instance already exists", instanceName));
                }

                Console.WriteLine(String.Format("Ensuring instance is running...", instanceName));
                var runningInstance = instance;
                while (runningInstance != null && runningInstance.State.Code != 16)
                {
                    Console.WriteLine(String.Format("Instance status: {0} ({1}). Waiting...", runningInstance.State.Name, runningInstance.State.Code));
                    System.Threading.Thread.Sleep(10 * 1000);

                    runningInstance = DeploymentService.GetInstanceById(instance.InstanceId);
                }
                Console.WriteLine("Instance is running!");
                Console.WriteLine();
                Console.WriteLine(String.Format("QlikApp can be accessed available at: {0}/qlikapp ",runningInstance.PublicDnsName));
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
