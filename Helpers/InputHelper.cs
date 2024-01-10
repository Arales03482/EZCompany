using System;

namespace EZCompanyMod.Helpers
{
    internal class InputHelper
    {
        internal static bool IsKeyDown(VKeys key)
        {
            return DataStore.GetAsyncKeyState(vKey: (int)key) < 0;
        }
    }
}
