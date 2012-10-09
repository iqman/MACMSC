using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Threading;
using Shared;
using Shared.Dto;
using Shared.ServiceContracts;
using Shared.ServiceProxy;

namespace S3CloudServices
{
    /// <summary>
    /// Cloud enabled WCF service host which uses a 32bit native dll
    /// http://blogs.msdn.com/b/haniatassi/archive/2009/03/20/using-a-32bit-dll-in-the-windows-azure.aspx
    /// http://msdn.microsoft.com/en-us/wazplatformtrainingcourse_windowsazurenativecodevs2010_topic2#_Toc303061204
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class PreService : IPreService
    {
        private const string preHostPath = "32bitPreHoster\\PreHoster.exe";
        private const string PreHostAddress = "net.pipe://localhost/pre";

        public void ResetLibPre()
        {
            try
            {
                KillPreHost();
            }
            catch (Exception e)
            {
                Logger.LogError("Error resetting libPre", e);
                throw;
            }
        }

        public DateTime GetServiceStartTime()
        {
            try
            {
                IPreService proxy = CreateProxy();
                return proxy.GetServiceStartTime();
            }
            catch (Exception e)
            {
                Logger.LogError("Error getting service start time", e);
                throw;
            }
        }

        public KeyPair GenerateKeyPair()
        {
            try
            {
                IPreService proxy = CreateProxy();
                return proxy.GenerateKeyPair();
            }
            catch (Exception e)
            {
                Logger.LogError("Error generating key pair", e);
                throw;
            }
        }

        public byte[] Encrypt(byte[] publicKey, byte[] plaintext)
        {
            try
            {
                IPreService proxy = CreateProxy();
                return proxy.Encrypt(publicKey, plaintext);
            }
            catch (Exception e)
            {
                Logger.LogError("Error encrypting", e);
                throw;
            }
        }

        public byte[] Decrypt(byte[] privateKey, byte[] ciphertext)
        {
            try
            {
                IPreService proxy = CreateProxy();
                return proxy.Decrypt(privateKey, ciphertext);
            }
            catch (Exception e)
            {
                Logger.LogError("Error decrypting", e);
                throw;
            }
        }

        public byte[] GenerateDelegationKey(byte[] privateKeyForDelegator, byte[] publicKeyForDelegatee)
        {
            try
            {
                IPreService proxy = CreateProxy();
                return proxy.GenerateDelegationKey(privateKeyForDelegator, publicKeyForDelegatee);
            }
            catch (Exception e)
            {
                Logger.LogError("Error generating delegation key", e);
                throw;
            }
        }

        public byte[] Reencrypt(byte[] delegationKey, byte[] cipherText)
        {
            try
            {
                IPreService proxy = CreateProxy();
                return proxy.Reencrypt(delegationKey, cipherText);
            }
            catch (Exception e)
            {
                Logger.LogError("Error reencrypting", e);
                throw;
            }
        }

        private static IPreService CreateProxy()
        {
            // Make sure that our dll host is running
            EnsurePreHostRunning();

            return new PreServiceProxy(ProxyFactory.CreateNamedPipeBinding(), PreHostAddress);
        }

        private static void EnsurePreHostRunning()
        {
            Process[] p = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(preHostPath));
            if (p.Length == 0)
            {
                Logger.LogInfo("Pre 32bit process was not found. Starting it...");
                ProcessStartInfo psi = new ProcessStartInfo(FilePathUtility.GetFullPath(preHostPath));
                Process dllHost = Process.Start(psi);

                Thread.Sleep(5000);  // give process time to start
            }
        }

        private static void KillPreHost()
        {
            Process[] p = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(preHostPath));
            if (p.Length == 1)
            {
                p[0].Kill();
            }
        }
    }
}
