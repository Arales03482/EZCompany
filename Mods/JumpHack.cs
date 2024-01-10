using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class JumpHack : MonoBehaviour
    {
        internal static JumpHack instance;

        public void Start()
        {
            instance = this;
        }

        internal void Run()
        {
            toggle = !toggle;

            if (!toggle)
                if (DataStore.mainController != default && DataStore.mainController != null)
                    DataStore.mainController.jumpForce = 5f;
        }

        public void Update()
        {
            if (toggle)
                if (DataStore.mainController != default && DataStore.mainController != null)
                    DataStore.mainController.jumpForce = GUIHandler.slider2;
        }

        internal static bool toggle = false;
    }
}
