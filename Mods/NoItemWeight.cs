using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class NoItemWeight : MonoBehaviour
    {
        internal static NoItemWeight instance;

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
                    DataStore.mainController.carryWeight = 1f;
                }
            }
        }

        internal static bool toggle = false;
    }
}
