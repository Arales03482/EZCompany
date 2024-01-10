using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace EZCompanyMod.Patches
{
    [HarmonyPatch]
    internal class PlayerPatch
    {
        [HarmonyPatch(typeof(PlayerControllerB), "Jump_performed")]
        [HarmonyPrefix]
        internal static bool JumpPatch(PlayerControllerB __instance)
        {
            if (Mods.FlyMode.toggle)
                return false;

            return true;
        }

        [HarmonyPatch(typeof(PlayerControllerB), "DamagePlayer")]
        [HarmonyPrefix]
        internal static bool FallDamagePatch(PlayerControllerB __instance, int damageNumber, bool hasDamageSFX = true, bool callRPC = true, CauseOfDeath causeOfDeath = CauseOfDeath.Unknown, int deathAnimation = 0, bool fallDamage = false, Vector3 force = default(Vector3))
        {
            if (causeOfDeath == CauseOfDeath.Gravity && Mods.NoFallDamage.toggle)
                return false;

            if (causeOfDeath == CauseOfDeath.Bludgeoning && Mods.NoShovelDamage.toggle)
                return false;

            return true;
        }
    }
}
