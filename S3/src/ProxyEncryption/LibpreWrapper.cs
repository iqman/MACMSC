using System;
using System.Runtime.InteropServices;

namespace ProxyEncryption
{
    internal enum CiphertextType
    {
        FirstLevel = 0,
        SecondLevel = 1,
        Reencrypted = 2,
    }

    public enum Scheme
    {
        Pre1 = 0,
        Pre2 = 1,
    }

    internal enum SerializeMode
    {
        Binary = 0,
        HexAscii = 1,
    }

    internal enum ErrorId
    {
        None = 0,
        PlaintextTooLong = 1,
        Other = 100,
    }

    internal static class LibpreWrapper
    {
        //int proxylib_initLibrary(char *seedbuf, int bufsize);
        [DllImport("libpre.dll", EntryPoint = "proxylib_initLibrary", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId InitLibrary(byte[] seedbuf, int bufsize);



        //int proxylib_generateParams(void **params, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_generateParams", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId GenerateParams(out IntPtr par, Scheme scheme);

        //int proxylib_serializeParams(void *params, char *buffer, int *bufferSize, int bufferAvailSize, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_serializeParams", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId SerializeParams(IntPtr par, byte[] buffer, out int bytesWritten, int bufferSize, Scheme scheme);

        //int proxylib_deserializeParams(char *buffer, int bufferSize, void **params, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_deserializeParams", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId DeserializeParams(byte[] buffer, int bufferSize, out IntPtr par, Scheme scheme);

        //int proxylib_destroyParams(void *params);
        [DllImport("libpre.dll", EntryPoint = "proxylib_destroyParams", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId DestroyParams(IntPtr par);



        //int proxylib_generateKeys(void *params, void **pk, void **sk, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_generateKeys", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId GenerateKeys(IntPtr par, out IntPtr publicKey, out IntPtr privateKey, Scheme scheme);

        //int proxylib_serializePublicKey(void *params, void *pk, char *pkBuf, int *pkBufSize, int bufferAvailSize, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_serializePublicKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId SerializePublicKey(IntPtr par, IntPtr publicKey, byte[] publicKeyBuffer, out int publicKeyBytesWritten, int bufferSize, Scheme scheme);

        //int proxylib_serializePrivateKey(void *params, void *sk, char *skBuf, int *skBufSize, int bufferAvailSize, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_serializePrivateKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId SerializePrivateKey(IntPtr par, IntPtr privateKey, byte[] privateKeyBuffer, out int privateKeyBytesWritten, int bufferSize, Scheme scheme);
        
        //int proxylib_deserializePublicKey(void *params, char *pkBuf, int pkBufSize, void **pk, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_deserializePublicKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId DeserializePublicKey(IntPtr par, byte[] buffer, int bufferSize, out IntPtr publicKey, Scheme scheme);

        //int proxylib_deserializePrivateKey(void *params, char *skBuf, int skBufSize, void **sk, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_deserializePrivateKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId DeserializePrivateKey(IntPtr par, byte[] buffer, int bufferSize, out IntPtr privateKey, Scheme scheme);

        //int proxylib_destroyKeys(void *pk, void *sk, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_destroyKeys", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId DestroyKeys(IntPtr par, IntPtr publicKey, IntPtr privateKey, Scheme scheme);



        //int proxylib_encrypt(void *params, void *pk, char *message, int messageLen, char *ciphertext, int *ciphLen, CIPHERTEXT_TYPE ctype, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_encrypt", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId Encrypt(IntPtr par, IntPtr publicKey, byte[] data, int dataSize, byte[] cipherText, ref int cipherTextSize, CiphertextType ciphertextType, Scheme scheme);

        //int proxylib_decrypt(void *params, void *sk, char *message, int *messageLen, char *ciphertext, int ciphLen, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_decrypt", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId Decrypt(IntPtr par, IntPtr privateKey, byte[] data, ref int dataSize, byte[] cipherText, int cipherTextSize, Scheme scheme);



        //int proxylib_generateDelegationKey(void *params, void *sk1, void *pk2, void** delKey, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_generateDelegationKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId GenerateDelegationKey(IntPtr par, IntPtr privateKeyForDelegator, IntPtr publicKeyForDelegatee, out IntPtr delegationKey, Scheme scheme);

        //int proxylib_serializeDelegationKey(void *params, void *delKey, char *delKeyBuf, int *delKeyBufSize, int bufferAvailSize, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_serializeDelegationKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId SerializeDelegationKey(IntPtr par, IntPtr delegationKey, byte[] buffer, out int bufferBytesWritten, int bufferSize, Scheme scheme);

        //int proxylib_deserializeDelegationKey(char *buffer, int bufferSize, void **delKey, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_deserializeDelegationKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId DeserializeDelegationKey(byte[] buffer, int bufferSize, out IntPtr delegationKey, Scheme scheme);

        //int proxylib_destroyDelegationKey(void *delKey, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_destroyDelegationKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId DestroyDelegationKey(IntPtr delegationKey, Scheme scheme);
        
        //int proxylib_reencrypt(void *params, void *rk, char *ciphertext, int ciphLen, char *newciphertext, int *newCiphLen, SCHEME_TYPE schemeID);
        [DllImport("libpre.dll", EntryPoint = "proxylib_reencrypt", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorId Reencrypt(IntPtr par, IntPtr delegationKey, byte[] cipherText, int cipherTextSize, byte[] newCipherText, ref int newCipherTextSize, Scheme scheme);
    }
}
