using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class ClimbHack : MonoBehaviour
    {
        internal static ClimbHack instance;

        public void Start()
        {
            instance = this;
        }

        internal void Run()
        {
            toggle = !toggle;

            if (!toggle)
                if (DataStore.mainController != default && DataStore.mainController != null)
                    DataStore.mainController.grabDistance = 4f;
        }

        public void Update()
        {
            if (toggle)
                if (DataStore.mainController != default && DataStore.mainController != null)
                    DataStore.mainController.climbSpeed = GUIHandler.slider3;
        }

        internal static bool toggle = false;
    }
}
