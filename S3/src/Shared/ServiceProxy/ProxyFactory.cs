using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Shared.ServiceProxy
{
    /// <summary>
    /// Controller for the service proxy.
    /// </summary>
    public static class ProxyFactory
    {
        private static Binding binding;
        
        private class ProxyInfo
        {
            public string ServiceUrl { get; private set; }
            public Type ProxyImplementationType { get; private set; }

            public ProxyInfo(string serviceUrl, Type proxyImplementationType)
            {
                ServiceUrl = serviceUrl;
                this.ProxyImplementationType = proxyImplementationType;
            }
        }

        private static readonly IDictionary<Type, ProxyInfo> proxyTypes = new Dictionary<Type, ProxyInfo>();

        /// <summary>
        /// Default constructor
        /// </summary>
        static ProxyFactory()
        {
            binding = CreateBinding();
        }

        public static Binding CreateNamedPipeBinding()
        {
            NetNamedPipeBinding b = new NetNamedPipeBinding();

            b.CloseTimeout = TimeSpan.FromMinutes(1);
            b.OpenTimeout = TimeSpan.FromMinutes(1);
            b.ReceiveTimeout = TimeSpan.FromMinutes(10);
            b.SendTimeout = TimeSpan.FromMinutes(10);

            b.MaxBufferSize = int.MaxValue;
            b.MaxReceivedMessageSize = int.MaxValue;
            b.MaxBufferPoolSize = 536000;

            b.ReaderQuotas.MaxArrayLength = int.MaxValue;
            b.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            b.ReaderQuotas.MaxDepth = int.MaxValue;
            b.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
            b.ReaderQuotas.MaxStringContentLength = int.MaxValue;

            return b;
        }

        public static Binding CreateBinding()
        {
            BasicHttpBinding b = new BasicHttpBinding();

            b.CloseTimeout = TimeSpan.FromMinutes(1);
            b.OpenTimeout = TimeSpan.FromMinutes(1);
            b.ReceiveTimeout = TimeSpan.FromMinutes(10);
            b.SendTimeout = TimeSpan.FromMinutes(10);

            b.MaxBufferSize = int.MaxValue;
            b.MaxReceivedMessageSize = int.MaxValue;
            b.MaxBufferPoolSize = 536000;

            b.ReaderQuotas.MaxArrayLength = int.MaxValue;
            b.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            b.ReaderQuotas.MaxDepth = int.MaxValue;
            b.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
            b.ReaderQuotas.MaxStringContentLength = int.MaxValue;

            return b;
        }

        /// <summary>
        /// Registers a new proxy interface/implementation pair.
        /// </summary>
        public static void RegisterProxy(Type proxyInterface, Type proxyImpl, string proxyUrl)
        {
            proxyTypes[proxyInterface] = new ProxyInfo(proxyUrl, proxyImpl);
        }

        /// <summary>
        /// Creates a new service proxy instance ready for communication.
        /// </summary>
        public static T CreateProxy<T>() where T : class
        {
            return CreateProxy<T>(null);
        }

        /// <summary>
        /// Creates a new service proxy instance ready for communication.
        /// </summary>
        public static T CreateProxy<T>(string proxyUrlOverride) where T : class
        {
            if (proxyTypes.ContainsKey(typeof(T)))
            {
                ProxyInfo info = proxyTypes[typeof(T)];

                if (string.IsNullOrEmpty(proxyUrlOverride))
                {
                    return (T)Activator.CreateInstance(info.ProxyImplementationType, binding, info.ServiceUrl);
                }

                return (T)Activator.CreateInstance(info.ProxyImplementationType, binding, proxyUrlOverride);
            }

            throw new InvalidOperationException("Cannot create type as it has not been registred in factory");
        }
    }
}
