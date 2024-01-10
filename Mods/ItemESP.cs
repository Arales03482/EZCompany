using UnityEngine;

namespace EZCompanyMod.Patches
{
    internal class ItemESP : MonoBehaviour
    {
        internal static ItemESP instance;

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

        public void OnGUI()
        {
            if (toggle && RoundManager.Instance != null)
            {
                foreach (EnemyAI enemy in RoundManager.Instance.SpawnedEnemies)
                {
                    if (DataStore.mainController != default && DataStore.mainController != null && enemy != default && enemy != null)
                    {
                        //In-Game Position
                        Vector3 pivotPos = enemy.transform.position; //Pivot point NOT at the feet, at the center
                        if (DataStore.mainController.transform.position != pivotPos)
                        {
                            Vector3 enemyFootPos; enemyFootPos.x = pivotPos.x; enemyFootPos.z = pivotPos.z; enemyFootPos.y = pivotPos.y - 2f; //At the feet
                            Vector3 enemyHeadPos; enemyHeadPos.x = pivotPos.x; enemyHeadPos.z = pivotPos.z; enemyHeadPos.y = pivotPos.y + 2f; //At the head

                            //Screen Position
                            bool w2s_footpos_success = Helpers.RenderHelper.WorldToScreen(DataStore.camera, enemyFootPos, out Vector3 w2s_footpos);
                            bool w2s_headpos_success = Helpers.RenderHelper.WorldToScreen(DataStore.camera, enemyHeadPos, out Vector3 w2s_headpos);

                            if (w2s_footpos_success && w2s_headpos_success)
                                Helpers.RenderHelper.DrawBoxESP(w2s_footpos, w2s_headpos, Color.red);
                        }
                    }
                }
            }
        }

        internal static bool toggle = false;
    }
}
