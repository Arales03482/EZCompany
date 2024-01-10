using HarmonyLib;

namespace EZCompanyMod.Patches
{
    internal class HauntedMaskPatch
    {
        [HarmonyPatch(typeof(HauntedMaskItem), nameof(HauntedMaskItem.ItemActivate))]
        [HarmonyPrefix]
        private static void Patch(HauntedMaskItem __instance)
        {
            if (Mods.GodMode.toggle)
            {
                return;
            }
        }
    }
}
