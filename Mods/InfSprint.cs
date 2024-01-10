using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class InfSprint : MonoBehaviour
    {
        internal static InfSprint instance;

        public void Start()
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
                    DataStore.mainController.sprintMeter = 5f;
                }
            }
        }

        internal static bool toggle = false;
    }
}
