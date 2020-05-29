using System;
using System.Runtime.InteropServices;
using System.Security;

namespace P42.IO.Helper
{
    public static class SystemHelper
    {
        public static string SecureStringToString(SecureString value)
        {
            var valuePtr = IntPtr.Zero;

            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
