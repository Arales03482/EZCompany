using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class FullBright : MonoBehaviour
    {
        internal static FullBright instance;

        internal static Light light;

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
            if (DataStore.mainController != default && DataStore.mainController != null)
            {
                if (!light || !DataStore.mainController.GetComponent<Light>())
                {
                    light = DataStore.mainController.gameObject.AddComponent<Light>();
                }

                light.color = new Color(1f, 1f, 1f, 0.5f);
                light.colorTemperature = 0f;
                light.innerSpotAngle = 179f;
                light.intensity = 5f;
                light.range = 1000000f;
                light.shadows = LightShadows.None;
                light.shape = LightShape.Cone;
                light.spotAngle = 179f;
                light.type = LightType.Directional;

                light.enabled = toggle;
            }

            if (DataStore.camera != default && DataStore.camera != null)
            {
                if (toggle)
                {
                    DataStore.camera.cameraType = CameraType.Preview;
                }
                else if (!toggle)
                {
                    DataStore.camera.cameraType = CameraType.Game;
                }
            }
        }

        internal static bool toggle = false;
    }
}
