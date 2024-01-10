using System;
using UnityEngine;

namespace EZCompanyMod.Helpers
{
    internal class VectorHelper
    {
        internal static Vector3 FixVector(Vector3 vec3)
        {
            return new Vector3((float)Math.Cos(Math.Atan2(vec3.z, vec3.x)), 0, (float)Math.Sin(Math.Atan2(vec3.z, vec3.x)));
        }
    }
}
