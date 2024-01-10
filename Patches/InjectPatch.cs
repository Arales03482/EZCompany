using HarmonyLib;

namespace EZCompanyMod.Patches
{
    [HarmonyPatch]
    internal class InjectPatch
    {
        [HarmonyPatch(typeof(RoundManager), "Awake")]
        [HarmonyPostfix]
        private static void AwakePatch(RoundManager __instance)
        {
            __instance.gameObject.AddComponent<GUIHandler>();
            __instance.gameObject.AddComponent<Mods.GodMode>();
            __instance.gameObject.AddComponent<Mods.InfTime>();
            __instance.gameObject.AddComponent<Mods.InfSprint>();
            __instance.gameObject.AddComponent<Mods.InfGrabReach>();
            __instance.gameObject.AddComponent<Mods.InfDeadlineTime>();
            __instance.gameObject.AddComponent<Mods.NoItemWeight>();
            __instance.gameObject.AddComponent<Mods.NoFallDamage>();
            __instance.gameObject.AddComponent<Mods.NoShovelDamage>();
            __instance.gameObject.AddComponent<Mods.SpeedHack>();
            __instance.gameObject.AddComponent<Mods.JumpHack>();
            __instance.gameObject.AddComponent<Mods.ClimbHack>();
            __instance.gameObject.AddComponent<Mods.CameraDeathVerifier>();
            __instance.gameObject.AddComponent<Mods.PlayerESP>();
            __instance.gameObject.AddComponent<Mods.MonsterESP>();
            __instance.gameObject.AddComponent<Mods.FlyMode>();
            __instance.gameObject.AddComponent<Mods.TPMainEntrance>();
            __instance.gameObject.AddComponent<Mods.TPShipItems>();
            __instance.gameObject.AddComponent<Mods.QuadroupleHanded>();
            __instance.gameObject.AddComponent<Mods.InfGroupCredits>();
            __instance.gameObject.AddComponent<Mods.AntiFire>();
            __instance.gameObject.AddComponent<Mods.FullBright>();
        }
    }
}
