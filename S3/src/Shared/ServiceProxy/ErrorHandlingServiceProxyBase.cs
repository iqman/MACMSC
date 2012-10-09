using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace Shared.ServiceProxy
{
    /// <summary>
    /// Generic base class for service proxies which adds error handling.
    /// </summary>
    public class ErrorHandlingServiceBase<T> : ClientBase<T> where T : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ErrorHandlingServiceBase(Binding binding, string webServiceUrl)
            : base(binding, new EndpointAddress(webServiceUrl))
        {
        }

        /// <summary>
        /// Invokes a method on the service proxy object in a way which includes error handling.
        /// </summary>
        protected TReturn InvokeWithErrorHandling<TReturn>(Func<TReturn> action)
        {
            try
            {
                TReturn result = action();
                Close();
                return result;
            }
            catch (FaultException<ServiceOperationException> feErt)
            {
                Logger.LogError("Error invoking server", feErt);
                Abort();

                throw new ServiceOperationException(feErt.Message, feErt);
            }
            catch (FaultException fe)
            {
                Logger.LogError("Error invoking server", fe);
                Abort();

                throw new ServiceOperationException(fe.Message, fe);
            }
            catch (CommunicationObjectAbortedException ce)
            {
                throw new ServiceOperationException("Operation aborted", ce);
            }
            catch (ActionNotSupportedException anse)
            {
                Logger.LogError("Error invoking server", anse);
                Abort();

                throw new ServiceOperationException("Service operation is not supported by the server", anse);
            }
            catch(ServiceActivationException saex)
            {
                Logger.LogError("The server is not correctly configured as the WCF service could not be activated", saex);
                throw new ServiceOperationException("Service operation failed because of bad server configuration", saex);
            }
            catch (MessageSecurityException msex)
            {
                Logger.LogError("Service operation failed because of a security issue", msex);

                throw new ServiceOperationException("The service operation failed because of a security issue", msex);
            }
            catch (Exception e)
            {
                Logger.LogError("Error invoking server", e);
                Abort();

                throw new ServiceOperationException("Service operation failed: " + e.Message, e);
            }
        }

        /// <summary>
        /// Invokes a method on the service proxy object in a way which includes error handling.
        /// </summary>
        protected void InvokeWithErrorHandling(Action action)
        {
            try
            {
                action();
                Close();
            }
            catch (FaultException<ServiceOperationException> feErt)
            {
                Logger.LogError("Error invoking server", feErt);
                Abort();

                throw new ServiceOperationException(feErt.Message, feErt);
            }
            catch (FaultException fe)
            {
                Logger.LogError("Error invoking server", fe);
                Abort();

                throw new ServiceOperationException(fe.Message, fe);
            }
            catch (CommunicationObjectAbortedException ce)
            {
                throw new ServiceOperationException("Operation aborted", ce);
            }
            catch (ActionNotSupportedException anse)
            {
                Logger.LogError("Error invoking server", anse);
                Abort();

                throw new ServiceOperationException("Service operation is not supported by the server", anse);
            }
            catch (ServiceActivationException saex)
            {
                Logger.LogError("The server is not correctly configured as the WCF service could not be activated", saex);
                throw new ServiceOperationException("Service operation failed because of bad server configuration", saex);
            }
            catch (MessageSecurityException msex)
            {
                Logger.LogError("Service operation failed because of a security issue", msex);

                throw new ServiceOperationException("The service operation failed because of a security issue", msex);
            }
            catch (Exception e)
            {
                Logger.LogError("Error invoking server", e);
                Abort();

                throw new ServiceOperationException("Service operation failed: " + e.Message, e);
            }
        }
    }
}
