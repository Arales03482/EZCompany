using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class AntiFire : MonoBehaviour
    {
        internal static AntiFire instance;

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
            if (toggle && DataStore.playerManager != default && DataStore.playerManager != null)
                DataStore.playerManager.firingPlayersCutsceneRunning = false;
        }

        internal static bool toggle = false;
    }
}
