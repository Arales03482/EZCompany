using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class SpeedHack : MonoBehaviour
    {
        internal static SpeedHack instance;

        public void Start()
        {
            instance = this;
        }

        internal void Run()
        {
            toggle = !toggle;

            if (!toggle)
                if (DataStore.mainController != default && DataStore.mainController != null)
                    DataStore.mainController.movementSpeed = 4.6f;
        }

        public void Update()
        {
            if (toggle)
                if (DataStore.mainController != default && DataStore.mainController != null)
                    DataStore.mainController.movementSpeed = GUIHandler.slider1;
        }

        internal static bool toggle = false;
    }
}
