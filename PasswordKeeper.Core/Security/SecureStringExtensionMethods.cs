using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.Core
{
    internal static class SecureStringExtensionMethods
    {
        internal static string Unsecure(this SecureString secureString)
        {
            if (secureString == null)
                return string.Empty;            
            var unmanagedString = IntPtr.Zero;
            try
            {
                // Unsecures the password
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // Clean up memory allocation
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
