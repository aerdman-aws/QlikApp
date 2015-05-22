using Amazon.EC2;
using AwsConsole.Services.Commands;
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
            //DeploymentService = new DeploymentService();
        }

        //protected DeploymentService DeploymentService { get; set; }

        public void DeployQlikAppInstance()
        {
            try
            {
                //DeploymentService.GetInstances();

                var setupSecurityGroup = new GetOrCreateSecurityGroupCommand();
                setupSecurityGroup.Execute();
            }
            catch (AmazonEC2Exception ex)
            {
                handleEC2Exception(ex);
            }
            
        }

        private void handleEC2Exception(AmazonEC2Exception ex)
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
    }
}
