using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace FinnAngelo.PomoFish
{
    internal static class NativeMethods
    {
        //https://docs.microsoft.com/en-us/visualstudio/code-quality/ca5122-p-invoke-declarations-should-not-be-safe-critical?view=vs-2017
        [SecuritySafeCritical]
        internal static void LockPC() => LockWorkStation();

        [SecurityCritical]
        //http://www.codeproject.com/Questions/337619/Lock-computer-using-csharp-in-window-application
        [DllImport("user32.dll")]
        private static extern void LockWorkStation();
    }
}
