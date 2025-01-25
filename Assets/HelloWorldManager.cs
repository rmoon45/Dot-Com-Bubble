using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Networking;
using UnityEngine;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;
using System;

namespace HelloWorld
{
    public class HelloWorldManager : MonoBehaviour
    {
        // public static NetworkManager m_NetworkManager;
        public TextMeshProUGUI iptext;

        public TextMeshProUGUI hostClienttext;
        public TextMeshProUGUI ipInput;

        public UnityTransport transport => (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;

        private string ip;

        void Awake()
        {
            //   m_NetworkManager = GetComponent<NetworkManager>();
            string ipa = GetLocalIPv4();
            iptext.text = $"IP:{ipa}";
        }

        public string GetLocalIPv4()
        {
            string address = Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.First(
            f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            .ToString();
            Debug.Log(address);
            return address;
        }

        public void StartHost()
        {
            transport.ConnectionData.Address = GetLocalIPv4();
            Debug.Log(GetLocalIPv4());
            NetworkManager.Singleton.StartHost();
            Debug.Log(transport.ConnectionData.Port);
        }

        public void StartClient()
        {
            if (ip != null && ip != "")
            {
                transport.ConnectionData.Address = ip;
                Debug.Log(ip);
                transport.SetConnectionData(ip, 7777);
            }
            NetworkManager.Singleton.StartClient();
        }

        private void Update()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsClient ? "Client" : "Not Connected";
            hostClienttext.text = mode;
        }

        public void SetIP(string ip)
        {
            this.ip = ip;
        }
        // void OnGUI()
        // {
        //     GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        //     if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer)
        //     {
        //         StartButtons();
        //     }
        //     else
        //     {
        //         StatusLabels();

        //         SubmitNewPosition();
        //     }

        //     GUILayout.EndArea();
        // }

        // static void StartButtons()
        // {
        //     if (GUILayout.Button("Host")) m_NetworkManager.StartHost();
        //     if (GUILayout.Button("Client")) m_NetworkManager.StartClient();
        //     if (GUILayout.Button("Server")) m_NetworkManager.StartServer();
        // }

        // static void StatusLabels()
        // {
        //     var mode = m_NetworkManager.IsHost ?
        //         "Host" : m_NetworkManager.IsServer ? "Server" : "Client";

        //     GUILayout.Label("Transport: " +
        //         m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);
        //     GUILayout.Label("Mode: " + mode);
        // }

        // static void SubmitNewPosition()
        // {
        //     if (GUILayout.Button(m_NetworkManager.IsServer ? "Move" : "Request Position Change"))
        //     {
        //         if (m_NetworkManager.IsServer && !m_NetworkManager.IsClient )
        //         {
        //             foreach (ulong uid in m_NetworkManager.ConnectedClientsIds);
        //               //  m_NetworkManager.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>().Move();
        //         }
        //         else
        //         {
        //             var playerObject = m_NetworkManager.SpawnManager.GetLocalPlayerObject();
        //             // var player = playerObject.GetComponent<HelloWorldPlayer>();
        //             // player.Move();
        //         }
        //     }
        // }
    }
}