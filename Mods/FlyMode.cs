using UnityEngine;

namespace EZCompanyMod.Mods
{
    internal class FlyMode : MonoBehaviour
    {
        internal static FlyMode instance;

        public void Start()
        {
            instance = this;
        }

        internal void Run()
        {
            toggle = !toggle;

            if (!toggle && DataStore.mainController != default && DataStore.mainController != null)
                DataStore.mainCharacterController.enabled = true;
        }

        internal void Set(float _speed)
        {
            speed = _speed;
        }

        public void Update()
        {
            if (toggle && Helpers.WindowHelper.WindowFocused && (DataStore.quickMenu == default || DataStore.quickMenu == null) && DataStore.mainController != default && DataStore.mainController != null && !DataStore.mainController.inTerminalMenu)
            {
                Transform transform = DataStore.camera.transform;
                Vector3 uvec = transform.up;
                Vector3 lvec = transform.forward;
                Vector3 rvec = transform.right;

                Vector3 direction = new Vector3(0, 0, 0);

                if (Helpers.InputHelper.IsKeyDown(VKeys.KEY_W))
                    direction += lvec;
                if (Helpers.InputHelper.IsKeyDown(VKeys.KEY_A))
                    direction -= rvec;
                if (Helpers.InputHelper.IsKeyDown(VKeys.KEY_S))
                    direction -= lvec;
                if (Helpers.InputHelper.IsKeyDown(VKeys.KEY_D))
                    direction += rvec;

                if (Helpers.InputHelper.IsKeyDown(VKeys.LSHIFT))
                    direction -= uvec;
                if (Helpers.InputHelper.IsKeyDown(VKeys.SPACE))
                    direction += uvec;

                direction = direction.normalized;

                if (direction != Vector3.zero)
                    DataStore.mainController.transform.position = DataStore.mainController.transform.position + (direction * (speed / 100f));

                DataStore.mainCharacterController.enabled = false;
            }
        }

        internal static bool toggle = false;

        internal static float speed = 10;
    }
}
