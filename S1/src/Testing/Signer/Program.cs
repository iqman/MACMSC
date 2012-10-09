using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Shared;
using Shared.Dto;

namespace Signer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Logger.Initialize();

                byte[] data = "Hello world".GetBytes();

                SignKeys signKeys = DataSigner.GenerateSignKeyPair();

                byte[] signature = DataSigner.Sign(data, signKeys.PublicAndPrivate);

                bool valid = DataSigner.IsSignatureValid(data, signature, signKeys.PublicOnly);
                Console.WriteLine(valid ? "Valid" : "NOT Valid");

                data[0] = 41;

                valid = DataSigner.IsSignatureValid(data, signature, signKeys.PublicOnly);
                Console.WriteLine(valid ? "Valid" : "NOT Valid");

                data[0] = "H".GetBytes()[0];

                valid = DataSigner.IsSignatureValid(data, signature, signKeys.PublicOnly);
                Console.WriteLine(valid ? "Valid" : "NOT Valid");
            }
            catch (Exception e)
            {
                Logger.LogError("Unhandled exception", e);
                Console.WriteLine("Error: " + e.Message);
            }

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
