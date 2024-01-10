using GameNetcodeStuff;
using UnityEngine;
using HarmonyLib;
using System.Collections;
using Unity.Netcode;
using System.Collections.Generic;

namespace EZCompanyMod
{
    internal class GUIHandler : MonoBehaviour
    {
        public void Start()
        {
            Helpers.LogHelper.Log("gui loaded!");
        }

        public void Update()
        {
            DataStore.mainController = GameNetworkManager.Instance.localPlayerController;
            DataStore.quickMenu = GameObject.Find("QuickMenu");
            DataStore.hangarShipAnimator = FindFirstObjectByType<ElevatorAnimationEvents>();
            DataStore.grabbableObjects = FindObjectsOfType<GrabbableObject>();
            DataStore.terminal = FindFirstObjectByType<Terminal>();

            DataStore.roundManager = RoundManager.Instance;
            DataStore.playerManager = StartOfRound.Instance;
            DataStore.timeData = TimeOfDay.Instance;

            if (DataStore.mainController != default && DataStore.mainController != null)
            {
                if (DataStore.mainController != DataStore.lastMainController)
                {
                    DataStore.lastMainController = DataStore.mainController;
                    DataStore.mainControllerTraverser = Traverse.Create(DataStore.mainController);
                    Helpers.LogHelper.Log("mainControllerTraverser changed");
                }

                DataStore.mainCharacterController = DataStore.mainController.GetComponent<CharacterController>();
                DataStore.camera = DataStore.mainController.gameplayCamera;
            }

            if (DataStore.terminal != default && DataStore.terminal != null)
            {
                DataStore.buyableItemsList = DataStore.terminal.buyableItemsList;

                DataStore.itemNames.Clear();
                for (int i = 0; i < DataStore.buyableItemsList.Length; i++)
                {
                    Item item = DataStore.buyableItemsList[i];
                    if (item != null)
                    {
                        DataStore.itemNames.Add(item.name, item);
                    }
                }

                DataStore.scrapList = "";
                foreach (KeyValuePair<string, Item> pair in DataStore.itemNames)
                {
                    DataStore.scrapList += "\"" + pair.Value.name + "\" ";
                }
                DataStore.scrapList = DataStore.scrapList.Remove(DataStore.scrapList.Length - 1, 1);
            }

            if (DataStore.playerManager != default && DataStore.playerManager != null)
            {
                foreach (SelectableLevel selectableLevel in DataStore.playerManager.levels)
                {
                    foreach (SpawnableItemWithRarity spawnableItemWithRarity in selectableLevel.spawnableScrap)
                    {
                        string name = spawnableItemWithRarity.spawnableItem.name;
                        if (!DataStore.scrapNames.ContainsKey(name))
                        {
                            DataStore.scrapNames.Add(name, spawnableItemWithRarity);
                        }
                    }

                    foreach (SpawnableEnemyWithRarity spawnableEnemyWithRarity in selectableLevel.Enemies)
                    {
                        string name = spawnableEnemyWithRarity.enemyType.name;
                        if (!DataStore.enemieNames.ContainsKey(name))
                        {
                            DataStore.enemieNames.Add(name, spawnableEnemyWithRarity);
                        }
                    }

                    foreach (SpawnableEnemyWithRarity spawnableEnemyWithRarity in selectableLevel.OutsideEnemies)
                    {
                        string name = spawnableEnemyWithRarity.enemyType.name;
                        if (!DataStore.enemieNames.ContainsKey(name))
                        {
                            DataStore.enemieNames.Add(name, spawnableEnemyWithRarity);
                        }
                    }

                    foreach (SpawnableEnemyWithRarity spawnableEnemyWithRarity in selectableLevel.DaytimeEnemies)
                    {
                        string name = spawnableEnemyWithRarity.enemyType.name;
                        if (!DataStore.enemieNames.ContainsKey(name))
                        {
                            DataStore.enemieNames.Add(name, spawnableEnemyWithRarity);
                        }
                    }
                }

                DataStore.itemList = "";
                foreach (KeyValuePair<string, SpawnableItemWithRarity> pair in DataStore.scrapNames)
                {
                    DataStore.itemList += "\"" + pair.Value.spawnableItem.name + "\" ";
                }
                DataStore.itemList = DataStore.itemList.Remove(DataStore.itemList.Length - 1, 1);
            }

            if (Helpers.WindowHelper.WindowFocused)
            {
                guiToggle[0] = Helpers.InputHelper.IsKeyDown(VKeys.INSERT);

                if (guiToggle[0] == false && guiToggle[1] == true)
                {
                    guiToggle[1] = false;
                }
                else if (guiToggle[0] == true && guiToggle[1] == false)
                {
                    guiToggle[2] = !guiToggle[2];

                    if (!guiToggle[2])
                    {
                        if (DataStore.quickMenu == default || DataStore.quickMenu == null)
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                        }
                    }

                    guiToggle[1] = true;
                }

                if (guiToggle[2])
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }

        private void OnDisable()
        {
            // If object will destroy in the end of current frame
            if (gameObject.activeInHierarchy)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Helpers.LogHelper.Log("gui unloaded!");
            }
        }

        public void OnGUI()
        {
            if (guiToggle[2])
            {
                Rect box_1 = new Rect(x: Screen.width - 500, y: 10, width: 490, height: 880);
                GUI.Box(box_1, "");

                string godmodeText = "Godmode: " + (Mods.GodMode.toggle ? "enabled" : "disabled");
                string infTimeText = "Infinite Time: " + (Mods.InfTime.toggle ? "enabled" : "disabled");
                string infSprintText = "Infinite Sprint: " + (Mods.InfSprint.toggle ? "enabled" : "disabled");
                string noItemWeightText = "No Item Weight: " + (Mods.NoItemWeight.toggle ? "enabled" : "disabled");
                string noFallDamageText = "No Fall Damage: " + (Mods.NoFallDamage.toggle ? "enabled" : "disabled");
                string speedHackText = "Speed Hack: " + (Mods.SpeedHack.toggle ? "enabled" : "disabled");
                string jumpHackText = "Jump Force: " + (Mods.JumpHack.toggle ? "enabled" : "disabled");
                string climbHackText = "Climb Speed: " + (Mods.ClimbHack.toggle ? "enabled" : "disabled");
                string infGrabReachText = "Infinite Grab Reach: " + (Mods.InfGrabReach.toggle ? "enabled" : "disabled");
                string deathVerifierText = "Death Verifier: " + (Mods.CameraDeathVerifier.toggle ? "enabled" : "disabled");
                string playerEspText = "Player ESP: " + (Mods.PlayerESP.toggle ? "enabled" : "disabled");
                string monsterEspText = "Monster ESP: " + (Mods.MonsterESP.toggle ? "enabled" : "disabled");
                string flyModeText = "Fly Mode: " + (Mods.FlyMode.toggle ? "enabled" : "disabled");
                string mainEntranceText = "TP Main Entrance: " + (Mods.TPMainEntrance.toggle ? "outside" : "inside");
                string shipItemsText = "TP Ship Items: " + (Mods.TPShipItems.toggle ? "ship items" : "outside ship items");
                string quadroupleHandedText = "Multi Handed: " + (Mods.QuadroupleHanded.toggle ? "enabled" : "disabled");
                string infGroupCreditsText = "Infinite Credits: " + (Mods.InfGroupCredits.toggle ? "enabled" : "disabled");
                string infDeadlineTimeText = "Infinite Deadline: " + (Mods.InfDeadlineTime.toggle ? "enabled" : "disabled");
                string antiFireText = "Anti Fire (host only): " + (Mods.AntiFire.toggle ? "enabled" : "disabled");
                string fullbrightText = "Fullbright: " + (Mods.FullBright.toggle ? "enabled" : "disabled");

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 10, width: 220, height: 20), godmodeText))
                {
                    Mods.GodMode.instance.Run();
                    Helpers.LogHelper.Log(godmodeText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 40, width: 220, height: 20), infTimeText))
                {
                    Mods.InfTime.instance.Run();
                    Helpers.LogHelper.Log(infTimeText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 70, width: 220, height: 20), infSprintText))
                {
                    Mods.InfSprint.instance.Run();
                    Helpers.LogHelper.Log(infSprintText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 100, width: 220, height: 20), noItemWeightText))
                {
                    Mods.NoItemWeight.instance.Run();
                    Helpers.LogHelper.Log(noItemWeightText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 130, width: 220, height: 20), noFallDamageText))
                {
                    Mods.NoFallDamage.instance.Run();
                    Helpers.LogHelper.Log(noFallDamageText);
                }

                GUI.Label(new Rect(x: box_1.x + 10, y: box_1.y + 151, width: 220, height: 20), "Speed: " + slider1.ToString());
                slider1 = GUI.HorizontalSlider(new Rect(x: box_1.x + 10, y: box_1.y + 170, width: 220, height: 20), slider1, 0f, 500f);

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 190, width: 220, height: 20), speedHackText))
                {
                    Mods.SpeedHack.instance.Run();
                    Helpers.LogHelper.Log(speedHackText);
                }

                GUI.Label(new Rect(x: box_1.x + 10, y: box_1.y + 211, width: 220, height: 20), "Jump Force: " + slider2.ToString());
                slider2 = GUI.HorizontalSlider(new Rect(x: box_1.x + 10, y: box_1.y + 230, width: 220, height: 20), slider2, 0f, 500f);

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 250, width: 220, height: 20), jumpHackText))
                {
                    Mods.JumpHack.instance.Run();
                    Helpers.LogHelper.Log(jumpHackText);
                }

                GUI.Label(new Rect(x: box_1.x + 10, y: box_1.y + 271, width: 220, height: 20), "Climb Speed: " + slider3.ToString());
                slider3 = GUI.HorizontalSlider(new Rect(x: box_1.x + 10, y: box_1.y + 290, width: 220, height: 20), slider3, 0f, 500f);

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 310, width: 220, height: 20), climbHackText))
                {
                    Mods.ClimbHack.instance.Run();
                    Helpers.LogHelper.Log(climbHackText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 340, width: 220, height: 20), infGrabReachText))
                {
                    Mods.InfGrabReach.instance.Run();
                    Helpers.LogHelper.Log(infGrabReachText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 370, width: 220, height: 20), deathVerifierText))
                {
                    Mods.CameraDeathVerifier.instance.Run();
                    Helpers.LogHelper.Log(deathVerifierText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 400, width: 220, height: 20), playerEspText))
                {
                    Mods.PlayerESP.instance.Run();
                    Helpers.LogHelper.Log(playerEspText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 430, width: 220, height: 20), monsterEspText))
                {
                    Mods.MonsterESP.instance.Run();
                    Helpers.LogHelper.Log(monsterEspText);
                }

                GUI.Label(new Rect(x: box_1.x + 10, y: box_1.y + 451, width: 220, height: 20), "Fly Speed: " + slider4.ToString());
                slider4 = GUI.HorizontalSlider(new Rect(x: box_1.x + 10, y: box_1.y + 470, width: 220, height: 20), slider4, 0f, 1000f);

                Mods.FlyMode.speed = slider4;
                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 490, width: 220, height: 20), flyModeText))
                {
                    Mods.FlyMode.instance.Run();
                    Helpers.LogHelper.Log(flyModeText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 520, width: 220, height: 20), "End Time"))
                {
                    Mods.InfTime.instance.EndTime();
                    Helpers.LogHelper.Log("EndTime");
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 550, width: 220, height: 20), "TP To Ship"))
                {
                    if (DataStore.mainController != default && DataStore.mainController != null)
                    {
                        DataStore.mainController.TeleportPlayer(DataStore.playerManager.playerSpawnPositions[0].position, false, 0f, false, true);
                    }
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 580, width: 220, height: 20), mainEntranceText))
                {
                    Mods.TPMainEntrance.instance.Run();
                    Helpers.LogHelper.Log(mainEntranceText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 610, width: 220, height: 20), "TP To Main Entrance"))
                {
                    if (DataStore.mainController != default && DataStore.mainController != null)
                    {
                        DataStore.mainController.TeleportPlayer(RoundManager.FindMainEntrancePosition(true, Mods.TPMainEntrance.toggle), false, 0f, false, true);
                    }
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 640, width: 220, height: 20), shipItemsText))
                {
                    Mods.TPShipItems.instance.Run();
                    Helpers.LogHelper.Log(shipItemsText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 670, width: 220, height: 20), "TP All Items"))
                {
                    PlayerControllerB player = DataStore.mainController;

                    if (player != default && player != null)
                    {
                        Vector3 vector = player.transform.position;

                        GrabbableObject[] array = FindObjectsOfType<GrabbableObject>();
                        for (int i = 0; i < array.Length; i++)
                        {
                            GrabbableObject item = array[i];
                            if (!item.isHeldByEnemy && !item.isHeld && ((!item.isInShipRoom && !item.isInElevator && !Mods.TPShipItems.toggle) || Mods.TPShipItems.toggle))
                            {
                                if (item.itemProperties.name != "Clipboard" && item.itemProperties.name != "StickyNote")
                                {
                                    item.transform.position = vector;
                                    item.targetFloorPosition = item.transform.localPosition;
                                }
                            }
                        }
                    }
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 700, width: 220, height: 20), quadroupleHandedText))
                {
                    Mods.QuadroupleHanded.instance.Run();
                    Helpers.LogHelper.Log(quadroupleHandedText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 730, width: 220, height: 20), infGroupCreditsText))
                {
                    Mods.InfGroupCredits.instance.Run();
                    Helpers.LogHelper.Log(infGroupCreditsText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 760, width: 220, height: 20), infDeadlineTimeText))
                {
                    Mods.InfDeadlineTime.instance.Run();
                    Helpers.LogHelper.Log(infDeadlineTimeText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 790, width: 220, height: 20), antiFireText))
                {
                    Mods.AntiFire.instance.Run();
                    Helpers.LogHelper.Log(antiFireText);
                }

                if (GUI.Button(new Rect(x: box_1.x + 10, y: box_1.y + 820, width: 220, height: 20), fullbrightText))
                {
                    Mods.FullBright.instance.Run();
                    Helpers.LogHelper.Log(fullbrightText);
                }

                GUI.Label(new Rect(x: box_1.x + 240, y: box_1.y + 9, width: 220, height: 20), "Item Name");
                text1 = GUI.TextField(new Rect(x: box_1.x + 240, y: box_1.y + 30, width: 220, height: 20), text1);

                if (GUI.Button(new Rect(x: box_1.x + 240, y: box_1.y + 60, width: 220, height: 20), "Spawn Selected Item"))
                {
                    if (DataStore.mainController != default && DataStore.mainController != null)
                    {
                        GameObject item = GameInterface.ItemManager.SpawnItem(text1, DataStore.mainController.transform.position);
                        //GameInterface.ItemManager.PickupItem(items[0]);
                    }
                }

                if (GUI.Button(new Rect(x: box_1.x + 240, y: box_1.y + 90, width: 220, height: 20), "Print Item List"))
                {
                    Helpers.LogHelper.Log(DataStore.scrapList);
                }

                GUI.Label(new Rect(x: box_1.x + 240, y: box_1.y + 109, width: 220, height: 20), "Scrap Name");
                text2 = GUI.TextField(new Rect(x: box_1.x + 240, y: box_1.y + 130, width: 220, height: 20), text2);
                GUI.Label(new Rect(x: box_1.x + 240, y: box_1.y + 159, width: 220, height: 20), "Scrap Amount");
                text3 = GUI.TextField(new Rect(x: box_1.x + 240, y: box_1.y + 180, width: 220, height: 20), text3);
                GUI.Label(new Rect(x: box_1.x + 240, y: box_1.y + 209, width: 220, height: 20), "Scrap Value (default -1)");
                text4 = GUI.TextField(new Rect(x: box_1.x + 240, y: box_1.y + 230, width: 220, height: 20), text4);

                if (GUI.Button(new Rect(x: box_1.x + 240, y: box_1.y + 260, width: 220, height: 20), "Spawn Selected Scrap"))
                {
                    if (DataStore.mainController != default && DataStore.mainController != null)
                    {
                        if (int.TryParse(text3, out int int1) && int.TryParse(text4, out int int2))
                        {
                            GameObject[] items = GameInterface.ItemManager.SpawnScrap(text2, DataStore.mainController.transform.position, int1, int2);
                            //GameInterface.ItemManager.PickupItem(items[0]);
                        }
                    }
                }

                if (GUI.Button(new Rect(x: box_1.x + 240, y: box_1.y + 290, width: 220, height: 20), "Print Scrap List"))
                {
                    Helpers.LogHelper.Log(DataStore.itemList);
                }
            }
        }

        internal static float slider1 = 4.6f;
        internal static float slider2 = 5f;
        internal static float slider3 = 4f;
        internal static float slider4 = 10f;
        internal static string text1 = "WalkieTalkie";
        internal static string text2 = "StopSign";
        internal static string text3 = "1";
        internal static string text4 = "-1";

        internal static bool[] guiToggle = { true, false, false };
    }
}
