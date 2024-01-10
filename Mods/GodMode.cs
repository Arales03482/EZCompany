using System;
using UnityEngine;
using GameNetcodeStuff;

namespace EZCompanyMod.Mods
{
    internal class GodMode : MonoBehaviour
    {
        internal static GodMode instance;

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
            if (DataStore.mainController != default && DataStore.mainController != null)
            {
                if (toggle)
                {
                    DataStore.mainController.health = 500;
                    StartOfRound.Instance.allowLocalPlayerDeath = false;
                }
                else
                {
                    DataStore.mainController.health = 100;
                    StartOfRound.Instance.allowLocalPlayerDeath = true;
                }
            }
        }

        internal static bool toggle = false;
    }
}
