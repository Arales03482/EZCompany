using BepInEx.Logging;
using System;

namespace EZCompanyMod.Helpers
{
    internal class LogHelper
    {
        internal static void Log(string content)
        {
            Plugin.logging.Log(LogLevel.Info, $"[{DateTime.Now}] [{PluginInfo.Name}] {content}");
        }
    }
}
