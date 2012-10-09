using System;
using System.Security.Cryptography;
using System.Text;

namespace Shared
{
    /// <summary>
    /// Class capable of creating guids.
    /// </summary>
    public static class GuidCreator
    {
        /// <summary>
        /// Creates a unique guid based on any string.
        /// </summary>
        public static Guid CreateGuidFromString(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException("input");

            HMACMD5 hmacmd5 = new HMACMD5("This is used as a static salt".GetBytes());

            byte[] inputData = input.GetBytes();
            byte[] hash = hmacmd5.ComputeHash(inputData);

            return new Guid(hash);
        }
    }
}
