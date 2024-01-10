using UnityEngine;
using GameNetcodeStuff;
using System;

namespace EZCompanyMod.Mods
{
    internal class PlayerESP : MonoBehaviour
    {
        internal static PlayerESP instance;

        public void Awake()
        {
            instance = this;
        }

        internal void Run()
        {
            toggle = !toggle;
        }

        public void Update()
        {
            
        }

        public void OnGUI()
        {
            if (toggle && DataStore.playerManager != null)
            {
                foreach (PlayerControllerB player in DataStore.playerManager.allPlayerScripts)
                {
                    if (player != default && player != null && player.IsLocalPlayer && !player.isPlayerDead && player.isPlayerControlled)
                    {
                        //In-Game Position
                        Vector3 pivotPos = player.transform.position; //Pivot point NOT at the feet, at the center
                        if (DataStore.mainController.transform.position != pivotPos)
                        {
                            Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - 2f; //At the feet
                            Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + 2f; //At the head

                            //Screen Position
                            bool w2s_footpos_success = Helpers.RenderHelper.WorldToScreen(DataStore.camera, playerFootPos, out Vector3 w2s_footpos);
                            bool w2s_headpos_success = Helpers.RenderHelper.WorldToScreen(DataStore.camera, playerHeadPos, out Vector3 w2s_headpos);

                            if (w2s_footpos_success && w2s_headpos_success)
                                Helpers.RenderHelper.DrawBoxESP(w2s_footpos, w2s_headpos, Color.green);
                        }
                    }
                }
            }
        }

        internal static bool toggle = false;
    }
}
