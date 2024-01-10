using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class InfDeadlineTime : MonoBehaviour
    {
        internal static InfDeadlineTime instance;

        public void Awake()
        {
            instance = this;
        }

        internal void Run()
        {
            toggle = !toggle;

            if (!toggle && DataStore.mainController != default && DataStore.mainController != null)
            {
                DataStore.timeData.timeUntilDeadline = 3240f; // DataStore.timeData.totalTime * 3f
                DataStore.timeData.UpdateProfitQuotaCurrentTime();
            }
        }

        public void Update()
        {
            if (toggle && DataStore.mainController != default && DataStore.mainController != null)
            {
                DataStore.timeData.timeUntilDeadline = 1080000f; // DataStore.timeData.totalTime * 1000f
                DataStore.timeData.UpdateProfitQuotaCurrentTime();
            }
        }

        internal static bool toggle = false;
    }
}
