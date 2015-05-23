using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using System.Threading;

namespace AwsConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            var awsDeployer = new AwsDeployer();
            awsDeployer.DeployQlikAppInstance();

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}