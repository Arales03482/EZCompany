using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class QuadroupleHanded : MonoBehaviour
    {
        internal static QuadroupleHanded instance;

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
            if (toggle && DataStore.mainController != default && DataStore.mainController != null)
                DataStore.mainController.twoHanded = false;
        }

        internal static bool toggle = false;
    }
}
