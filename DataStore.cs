using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EZCompanyMod
{
    internal class DataStore
    {
        internal static readonly Harmony harmony = new Harmony(PluginInfo.GUID);

        internal static PlayerControllerB mainController;
        internal static CharacterController mainCharacterController;
        internal static PlayerControllerB lastMainController;
        internal static Traverse mainControllerTraverser;
        internal static GameObject quickMenu;
        internal static ElevatorAnimationEvents hangarShipAnimator;
        internal static Camera camera;
        internal static GrabbableObject[] grabbableObjects = new GrabbableObject[0];
        internal static Terminal terminal;
        internal static Dictionary<string, SpawnableItemWithRarity> scrapNames = new Dictionary<string, SpawnableItemWithRarity> { };
        internal static Dictionary<string, Item> itemNames = new Dictionary<string, Item> { };
        internal static Dictionary<string, SpawnableEnemyWithRarity> enemieNames = new Dictionary<string, SpawnableEnemyWithRarity> { };
        internal static Item[] buyableItemsList;

        internal static string itemList = "";
        internal static string scrapList = "";

        internal static RoundManager roundManager;
        internal static StartOfRound playerManager;
        internal static TimeOfDay timeData;

        [DllImport("user32.dll")]
        internal extern static short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        internal static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern int GetWindowText(int hWnd, StringBuilder text, int count);
    }
}
