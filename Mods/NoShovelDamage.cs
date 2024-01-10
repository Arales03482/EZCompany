using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class NoShovelDamage : MonoBehaviour
    {
        internal static NoShovelDamage instance;

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
