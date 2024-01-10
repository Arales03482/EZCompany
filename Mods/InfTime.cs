using System;
using System.Windows.Input;
using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class InfTime : MonoBehaviour
    {
        internal static InfTime instance;

        public void Start()
        {
            instance = this;
        }

        internal void Run()
        {
            toggle = !toggle;
        }

        internal void EndTime()
        {
            toggle = false;
            TimeOfDay.Instance.globalTime = 10000.0f;
        }

        public void Update()
        {
            if (toggle)
            {
                TimeOfDay.Instance.globalTime = 0.0f;
            }
        }

        internal static bool toggle = false;
    }
}
