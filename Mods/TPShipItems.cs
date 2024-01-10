using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class TPShipItems : MonoBehaviour
    {
        internal static TPShipItems instance;

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

        }

        internal static bool toggle = false;
    }
}
