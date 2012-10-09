using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Shared;
using Shared.ServiceContracts;
using Shared.ServiceProxy;

namespace StorageHoster
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Uri baseAddress = new Uri("http://localhost:8080/storageGateway");

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

                using (ServiceHost host = new ServiceHost(typeof(GatewayService), baseAddress))
                {
                    // Enable metadata publishing.
                    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                    smb.HttpGetEnabled = true;
                    smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                    host.Description.Behaviors.Add(smb);

                    // Add application endpoint
                    host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                            MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                    host.AddServiceEndpoint(typeof(IGatewayService), ProxyFactory.CreateBinding(), "");

                    // Open the ServiceHost to start listening for messages. Since
                    // no endpoints are explicitly configured, the runtime will create
                    // one endpoint per base address for each service contract implemented
                    // by the service.
                    host.Open();

                    Logger.LogInfo("ServiceHost is ready...");

                    Console.WriteLine("The service is ready at {0}", baseAddress);
                    Console.WriteLine("Press <Enter> to stop the service.");
                    Console.ReadKey();

                    Console.WriteLine();

                    Logger.LogInfo("ServiceHost is closing...");

                    // Close the ServiceHost.
                    host.Close();

                    Console.WriteLine("Service has been closed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error hosting service. See log for details.");
                
                Logger.LogError("Unhandled exception in service hoster", e);
            }

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
