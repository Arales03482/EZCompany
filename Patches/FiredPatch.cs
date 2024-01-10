using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZCompanyMod.Patches
{
    [HarmonyPatch]
    internal class FiredPatch
    {
        [HarmonyPatch(typeof(StartOfRound), "FirePlayersAfterDeadlineClientRpc")]
        [HarmonyPrefix]
        internal static bool JumpPatch(StartOfRound __instance, int[] endGameStats, bool abridgedVersion = false)
        {
            if (Mods.AntiFire.toggle)
                return false;

            return true;
        }
    }
}
