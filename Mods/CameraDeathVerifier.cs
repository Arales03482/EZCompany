using System;
using UnityEngine;
using GameNetcodeStuff;

namespace EZCompanyMod.Mods
{
    internal class CameraDeathVerifier : MonoBehaviour
    {
        internal static CameraDeathVerifier instance;

        public void Start()
        {
            instance = this;
        }

        internal void Run()
        {
            toggle = !toggle;
        }

        internal void Update()
        {
            if (toggle)
            {
                ManualCameraRenderer manualCameraRenderer = StartOfRound.Instance.mapScreen;
                TransformAndName player = manualCameraRenderer.radarTargets[manualCameraRenderer.targetTransformIndex];
                if (player != null)
                {
                    PlayerControllerB component = player.transform.gameObject.GetComponent<PlayerControllerB>();
                    StartOfRound.Instance.mapScreenPlayerName.text = "MONITORING " + (component.isPlayerDead ? "DEAD" : "ALIVE") + ":\n" + player.name;
                }
            }
            else
            {
                ManualCameraRenderer manualCameraRenderer = StartOfRound.Instance.mapScreen;
                TransformAndName player = manualCameraRenderer.radarTargets[manualCameraRenderer.targetTransformIndex];
                if (player != null)
                {
                    PlayerControllerB component = player.transform.gameObject.GetComponent<PlayerControllerB>();
                    StartOfRound.Instance.mapScreenPlayerName.text = "MONITORING : " + player.name;
                }
            }
        }

        internal static bool toggle = false;
    }
}
