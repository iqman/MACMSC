using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared
{
    public static class StringByteArrayUtil
    {
        public static byte[] GetBytes(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        public static string GetString(this byte[] d)
        {
            return Encoding.UTF8.GetString(d);
        }
    }
}
