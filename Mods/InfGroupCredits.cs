using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class InfGroupCredits : MonoBehaviour
    {
        internal static InfGroupCredits instance;

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
            {
                DataStore.terminal.groupCredits = 100000000;
                DataStore.terminal.SyncGroupCreditsServerRpc(DataStore.terminal.groupCredits, DataStore.terminal.numberOfItemsInDropship);
            }
        }

        internal static bool toggle = false;
    }
}
