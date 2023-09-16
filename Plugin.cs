using System;
using System.Reflection;
using BepInEx;
using Epic.OnlineServices.AntiCheatClient;
using HarmonyLib;
using Landfall;
using UnityEngine;

namespace TabgNetworkStatisticsPanel
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            GameObject TabgNetworkStatisticsPanelGameObject = new GameObject("TabgNetworkStatisticsPanelGameObject");
            TabgNetworkStatisticsPanelGameObject.AddComponent<TabgNetworkStatisticsPanel>();
            DontDestroyOnLoad(TabgNetworkStatisticsPanelGameObject);
            Logger.LogInfo($"Net stats panel has been made!");
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll(typeof(CurrentRoomTextUIHandler_Patch));
            Logger.LogInfo($"CurrentRoomTextUIHandler has been patched!");

        }
    }

    public class CurrentRoomTextUIHandler_Patch
    {
        [HarmonyPatch(typeof(CurrentRoomTextUIHandler), "Start")]
        [HarmonyPrefix]
        public static bool Start(CurrentRoomTextUIHandler __instance)
        {
            UnityEngine.Object.Destroy(__instance.gameObject);
            return true;
        }
    }
}
