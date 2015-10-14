using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FinnAngelo.PomoFish
{
    internal static class NativeMethods
    {
        //http://www.codeproject.com/Questions/337619/Lock-computer-using-csharp-in-window-application
        [DllImport("user32.dll")]
        internal static extern void LockWorkStation();
    }
}
