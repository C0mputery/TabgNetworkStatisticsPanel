using Landfall.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TabgNetworkStatisticsPanel
{
    public class TabgNetworkStatisticsPanel : MonoBehaviour
    {
        bool mShowStats = false;
        public Rect statsRect = new Rect(0, Screen.height / 2 - 250, 300f, 500f);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                mShowStats = !mShowStats;
            }
        }

        public void OnGUI()
        {
            if (mShowStats && (bool)PhotonServerHandler.instance)
            {
                statsRect = GUILayout.Window(100, statsRect, StatsWindow, "Toggle (Q)");
            }
        }

        private void StatsWindow(int window)
        {
            GUILayout.Label("Current Server: " + ServerConnector.LastJoinedServerText);
            //GUILayout.Label("Local Player Is Admin: " + PhotonServerHandler.instance.LocalPlayer.IsAdmin);
            //GUILayout.Label("Time Since Last Package: " + PhotonServerHandler.instance.LocalPlayer.TimeSinceLastPackage());
            GUILayout.Label("PhotonServerHandler.instance.LocalPlayer.Health: " + PhotonServerHandler.instance.LocalPlayer.Health);

            string text = "";
            TABGPlayerClient[] playerList = PhotonServerHandler.instance.Players;
            foreach (TABGPlayerClient tabgPlayerClient in playerList)
            {
                string text2 = ("Name: " + ((!string.IsNullOrEmpty(tabgPlayerClient.PlayerName)) ? tabgPlayerClient.PlayerName : "[NO NAME]"));
                text2 += " | Is Admin: " + tabgPlayerClient.IsAdmin;
                text2 += " | Index: " + tabgPlayerClient.PlayerIndex;
                text2 = Regex.Replace(text2, @"\t|\n|\r", "");
                text = text + "\n" + text2;
            }
            GUILayout.Label("[PLAYERS]\n" + text);
            /*GUILayout.Label("Is Connected: " + PhotonNetwork.connected);
            GUILayout.Label("Photon Room: " + photonRoom);
            GUILayout.Label("Master Client: " + (!string.IsNullOrEmpty(PhotonNetwork.masterClient.NickName) ? PhotonNetwork.masterClient.NickName : "[NO NAME]"));
            GUILayout.Label("Zombies: " + NetworkZombieSpawner.SpawnedZombies);
            GUILayout.Label("Memory: " + System.GC.GetTotalMemory(false) / 1048576 + " MB");
            GUILayout.Label("FPS: " + (int)(1f / Time.unscaledDeltaTime));
            GUILayout.Label("Ping: " + PhotonNetwork.GetPing());
            GUILayout.Label("AVG msg/s: " + avgMessageCount);
            GUILayout.Space(5f);
            string text = "Players in Room: " + PhotonNetwork.room.PlayerCount;
            PhotonPlayer[] playerList = PhotonNetwork.playerList;
            foreach (PhotonPlayer photonPlayer in playerList)
            {
                string text2 = ((!string.IsNullOrEmpty(photonPlayer.NickName)) ? photonPlayer.NickName : "[NO NAME]");
                text2 = Regex.Replace(text2, @"\t|\n|\r", "");
                text = text + "\n" + text2;
            }
            GUILayout.Label("[PLAYERS]\n" + text);*/
        }
    }
}
