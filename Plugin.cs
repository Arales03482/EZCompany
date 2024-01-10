using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using System.Reflection;

namespace EZCompanyMod
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    internal class Plugin : BaseUnityPlugin
    {
        internal static Plugin instance;
        internal static ManualLogSource logging;
        //internal static GameObject LoadObj;

        public void Awake()
        {
            instance = this;
            logging = base.Logger;

            Helpers.ConsoleHelper.CreateConsole();
            Helpers.LogHelper.Log("loading..");

            DataStore.harmony.PatchAll(Assembly.GetExecutingAssembly());

            /*LoadObj = new GameObject();
            LoadObj.AddComponent<GUIHandler>();
            LoadObj.AddComponent<Mods.GodMode>();
            LoadObj.AddComponent<Mods.InfTime>();
            LoadObj.AddComponent<Mods.CameraDeathVerifier>();
            Object.DontDestroyOnLoad(LoadObj);*/

            Helpers.LogHelper.Log("loaded!");
        }

        public void Update()
        {
            Helpers.WindowHelper.WindowFocused = Helpers.WindowHelper.GetActiveWindowName() == System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }

        //public static void Unload() => Object.Destroy(LoadObj);
    }
}
