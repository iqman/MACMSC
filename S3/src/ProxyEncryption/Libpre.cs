using System;
using Shared;

namespace ProxyEncryption
{
    public class Libpre
    {
        private readonly byte[] tempBuf = new byte[1000];
        private readonly Scheme scheme;
        private readonly IntPtr parameters;

        private ErrorId errorId;

        static Libpre()
        {
            ErrorId e = LibpreWrapper.InitLibrary(null, 0);
            if (e != ErrorId.None) throw new InvalidOperationException("Initialization of LibPre failed");
        }

        public Libpre(Scheme scheme)
            : this(scheme, null)
        {
        }

        public Libpre(Scheme scheme, byte[] parameters)
        {
            this.scheme = scheme;

            // Initialize unmanaged library by having it generate parameters first.
            // This is required to avoid a weird error where it otherwise would crash
            // the entire process if initialized with parameters the first time.
            this.errorId = LibpreWrapper.GenerateParams(out this.parameters, this.scheme);
            AssertSuccess();

            // if we actually received some premade parameters, then use those)
            if (parameters != null)
            {
                LibpreWrapper.DestroyParams(this.parameters);
                this.parameters = DeserializeParams(parameters);
            }
        }

        public byte[] SerializeParameters()
        {
            int bytesWritten;
            this.errorId = LibpreWrapper.SerializeParams(this.parameters, this.tempBuf, out bytesWritten, this.tempBuf.Length, this.scheme);
            AssertSuccess();

            return this.tempBuf.RangeSubset(0, bytesWritten);
        }

        private IntPtr DeserializeParams(byte[] buffer)
        {
            IntPtr par;
            this.errorId = LibpreWrapper.DeserializeParams(buffer, buffer.Length, out par, this.scheme);
            AssertSuccess();

            return par;
        }

        //private void DeleteParams(IntPtr par)
        //{
        //    this.errorId = LibpreWrapper.DestroyParams(par);
        //    AssertSuccess();
        //}

        public InMemoryKeyPair GenerateKeyPair()
        {
            IntPtr publicKey;
            IntPtr privateKey;
            this.errorId = LibpreWrapper.GenerateKeys(this.parameters, out publicKey, out privateKey, this.scheme);
            AssertSuccess();

            return new InMemoryKeyPair(publicKey, privateKey);
        }

        public byte[] SerializePublicKey(IntPtr publicKey)
        {
            int bytesWritten;
            this.errorId = LibpreWrapper.SerializePublicKey(this.parameters, publicKey, this.tempBuf, out bytesWritten, this.tempBuf.Length, this.scheme);
            AssertSuccess();

            return this.tempBuf.RangeSubset(0, bytesWritten);
        }

        public byte[] SerializePrivateKey(IntPtr privateKey)
        {
            int bytesWritten;
            this.errorId = LibpreWrapper.SerializePrivateKey(this.parameters, privateKey, this.tempBuf, out bytesWritten, this.tempBuf.Length, this.scheme);
            AssertSuccess();

            return this.tempBuf.RangeSubset(0, bytesWritten);
        }

        public IntPtr DeserializePublicKey(byte[] buffer)
        {
            IntPtr publicKey;
            this.errorId = LibpreWrapper.DeserializePublicKey(this.parameters, buffer, buffer.Length, out publicKey, this.scheme);
            AssertSuccess();

            return publicKey;
        }

        public IntPtr DeserializePrivateKey(byte[] buffer)
        {
            IntPtr privateKey;
            this.errorId = LibpreWrapper.DeserializePrivateKey(this.parameters, buffer, buffer.Length, out privateKey, this.scheme);
            AssertSuccess();

            return privateKey;
        }

        public void DeleteKeyPair(InMemoryKeyPair keys)
        {
            this.errorId = LibpreWrapper.DestroyKeys(this.parameters, keys.PublicKey, keys.PrivateKey, this.scheme);
            AssertSuccess();
        }

        public void DeletePublicKey(IntPtr publicKey)
        {
            this.errorId = LibpreWrapper.DestroyKeys(this.parameters, publicKey, IntPtr.Zero, this.scheme);
            AssertSuccess();
        }

        public void DeletePrivateKey(IntPtr privateKey)
        {
            this.errorId = LibpreWrapper.DestroyKeys(this.parameters, IntPtr.Zero, privateKey, this.scheme);
            AssertSuccess();
        }

        public byte[] Encrypt(IntPtr publicKey, byte[] plaintext)
        {
            int cipherTextSize = this.tempBuf.Length;
            this.errorId = LibpreWrapper.Encrypt(this.parameters, publicKey, plaintext, plaintext.Length, this.tempBuf, ref cipherTextSize, CiphertextType.SecondLevel, this.scheme);
            AssertSuccess();

            return tempBuf.RangeSubset(0, cipherTextSize);
        }

        public byte[] Decrypt(IntPtr privateKey, byte[] ciphertext)
        {
            int dataSize = this.tempBuf.Length;
            this.errorId = LibpreWrapper.Decrypt(this.parameters, privateKey, this.tempBuf, ref dataSize, ciphertext, ciphertext.Length, this.scheme);
            AssertSuccess();

            return tempBuf.RangeSubset(0, dataSize);
        }

        public IntPtr GenerateDelegationKey(IntPtr privateKeyForDelegator, IntPtr publicKeyForDelegatee)
        {
            IntPtr delegationKey;
            this.errorId = LibpreWrapper.GenerateDelegationKey(this.parameters, privateKeyForDelegator, publicKeyForDelegatee, out delegationKey, this.scheme);
            AssertSuccess();

            return delegationKey;
        }

        public byte[] SerializeDelegationKey(IntPtr delegationKey)
        {
            int bytesWritten;
            this.errorId = LibpreWrapper.SerializeDelegationKey(this.parameters, delegationKey, this.tempBuf, out bytesWritten, this.tempBuf.Length, this.scheme);
            AssertSuccess();

            return tempBuf.RangeSubset(0, bytesWritten);
        }

        public IntPtr DeserializeDelegationKey(byte[] buffer)
        {
            IntPtr delegationKey;
            this.errorId = LibpreWrapper.DeserializeDelegationKey(buffer, buffer.Length, out delegationKey, this.scheme);
            AssertSuccess();

            return delegationKey;
        }

        public void DeleteDelegationKey(IntPtr delegationKey)
        {
            this.errorId = LibpreWrapper.DestroyDelegationKey(delegationKey, this.scheme);
            AssertSuccess();
        }

        public byte[] Reencrypt(IntPtr delegationKey, byte[] cipherText)
        {
            int newCipherTextSize = this.tempBuf.Length;
            this.errorId = LibpreWrapper.Reencrypt(this.parameters, delegationKey, cipherText, cipherText.Length, this.tempBuf, ref newCipherTextSize, this.scheme);
            AssertSuccess();

            return tempBuf.RangeSubset(0, newCipherTextSize);
        }

        private void AssertSuccess()
        {
            switch (this.errorId)
            {
                case ErrorId.None:
                    break;
                case ErrorId.PlaintextTooLong:
                    throw new Exception("Plaintext is too long");
                case ErrorId.Other:
                    throw new Exception("Unknown error");
                default:
                    throw new ArgumentOutOfRangeException("error");
            }
        }

        private bool disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).

                // TODO: Fix cleanup
                //LibpreWrapper.DestroyParams(this.parameters);
                //this.parameters = IntPtr.Zero;

                disposed = true;
            }
        }

        ~Libpre()
        {
            Dispose(false);
        }

    }
}
