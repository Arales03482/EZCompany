using UnityEngine;

namespace EZCompanyMod.Mods
{
    class TPMainEntrance : MonoBehaviour
    {
        internal static TPMainEntrance instance;

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
            
        }

        internal static bool toggle = false;
    }
}
