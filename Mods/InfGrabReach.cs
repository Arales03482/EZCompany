using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class InfGrabReach : MonoBehaviour
    {
        internal static InfGrabReach instance;

        public void Start()
        {
            instance = this;
        }

        internal void Run()
        {
            toggle = !toggle;

            if (!toggle)
                if (DataStore.mainController != default && DataStore.mainController != null)
                    DataStore.mainController.grabDistance = 5f;
        }

        public void Update()
        {
            if (toggle)
                if (DataStore.mainController != default && DataStore.mainController != null)
                    DataStore.mainController.grabDistance = 1000000f;
        }

        internal static bool toggle = false;
    }
}
