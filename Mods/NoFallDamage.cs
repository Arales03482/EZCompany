using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class NoFallDamage : MonoBehaviour
    {
        internal static NoFallDamage instance;

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
