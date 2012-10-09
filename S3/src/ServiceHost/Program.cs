using System;
using System.ServiceModel.Description;
using S3CloudServices;
using Shared;
using Shared.ServiceContracts;
using Shared.ServiceProxy;

namespace ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Uri storageBaseAddress = new Uri("http://localhost:8080/s3storageGateway");
                Uri preBaseAddress = new Uri("http://localhost:8080/s3pre");

                Logger.Initialize();
                Logger.LogInfo("ServiceHost is starting...");

                if (WinSecurity.IsVistaOrHigher() && !WinSecurity.IsAdmin())
                {
                    Console.WriteLine("This application requires administrator rights to work");
                    Console.WriteLine("Press any key to restart the application with administrative rights, or close the program to exit...");
                    Console.ReadKey();

                    Logger.LogInfo("Restarting process as admin");

                    WinSecurity.RestartElevated();
                    return;
                }

                System.ServiceModel.ServiceHost storageHost =
                    new System.ServiceModel.ServiceHost(typeof(StorageService), storageBaseAddress);

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                storageHost.Description.Behaviors.Add(smb);

                // Add application endpoint
                storageHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                        MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                storageHost.AddServiceEndpoint(typeof(IGatewayService), ProxyFactory.CreateBinding(), "");



                System.ServiceModel.ServiceHost preHost =
                    new System.ServiceModel.ServiceHost(typeof(PreService), preBaseAddress);

                smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                preHost.Description.Behaviors.Add(smb);

                // Add application endpoint
                preHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                        MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                preHost.AddServiceEndpoint(typeof(IPreService), ProxyFactory.CreateBinding(), "");

                preHost.Open();
                storageHost.Open();


                Logger.LogInfo("ServiceHosts are ready...");

                Console.WriteLine("The storage services is ready at {0}", storageBaseAddress);
                Console.WriteLine("The pre services is ready at {0}", preBaseAddress);
                Console.WriteLine("Press <Enter> to stop the services.");
                Console.ReadKey();

                Console.WriteLine();

                Logger.LogInfo("ServiceHosts are closing...");

                preHost.Close();
                storageHost.Close();

                Console.WriteLine("Services have been closed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error hosting services. See log for details.");
                Logger.LogError("Unhandled exception in service hoster", e);
            }

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
