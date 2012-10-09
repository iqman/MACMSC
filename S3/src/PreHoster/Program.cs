using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using Shared;
using Shared.ServiceContracts;
using Shared.ServiceProxy;

namespace PreHoster
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Logger.Initialize();

                Logger.LogInfo("Starting 32bit PreHoster");

                Uri baseAddress = new Uri("net.pipe://localhost/pre");
                Uri mexAddress = new Uri("net.pipe://localhost/pre/Mex");

                using (ServiceHost host = new ServiceHost(typeof(PreService)))
                {
                    host.AddServiceEndpoint(typeof(IPreService), ProxyFactory.CreateNamedPipeBinding(), baseAddress);

                    ServiceMetadataBehavior metadata = new ServiceMetadataBehavior();
                    host.Description.Behaviors.Add(metadata);
                    host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexNamedPipeBinding(), mexAddress);

                    host.Open();

                    Logger.LogInfo("Service is ready at " + baseAddress);

                    Console.WriteLine("The service is ready at {0}", baseAddress);
                    Console.WriteLine("Press <Enter> to stop the service.");
                    Console.ReadKey();

                    Console.WriteLine();

                    host.Close();

                    Console.WriteLine("Service has been closed");
                }
            }
            catch (Exception e)
            {
                Logger.LogError("Unhandled exception in 32bit PreHoster", e);
                Console.WriteLine("Error hosting service: ");
                LogException(e);
            }

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }

        /// <summary>
        /// Logs an exception by logging all the information in it.
        /// </summary>
        /// <param name="e">The exception to log</param>
        private static void LogException(Exception e)
        {
            StringBuilder exceptionText = new StringBuilder();

            exceptionText.AppendLine(e.GetType() + " " + e.Message);
            exceptionText.Append(e.StackTrace);

            Exception inner = e.InnerException;

            while (inner != null)
            {
                exceptionText.AppendLine();
                exceptionText.AppendLine(inner.GetType() + " " + inner.Message);
                exceptionText.Append(inner.StackTrace);

                inner = inner.InnerException;
            }

            Console.WriteLine(exceptionText.ToString());
        }
    }
}
