using System;
using System.Collections.Generic;
using System.Text;

namespace EZCompanyMod.Helpers
{
    internal class WindowHelper
    {
        /// <summary>Returns true if the current application has focus, false otherwise</summary>
        internal static string GetActiveWindowName()
        {
            const int nChars = 256;
            StringBuilder sb = new StringBuilder(nChars);
            int handle = DataStore.GetForegroundWindow();

            if (DataStore.GetWindowText(handle, sb, nChars) > 0)
                return sb.ToString();

            return null;
        }

        internal static bool WindowFocused = false;
    }
}
