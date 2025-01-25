using TMPro;
using Unity.Netcode;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Networking;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;
using System;
using Unity.VisualScripting;

public class Lobby : MonoBehaviour
{
    public TextMeshProUGUI ipInput;
    public TextMeshProUGUI hostIPText;
    public GameObject hostWaitingUI;
    public GameObject RoleSelectUI;

    private UnityTransport transport => (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
    private string ip;

    void Awake()
    {
        hostWaitingUI.SetActive(false);
        RoleSelectUI.SetActive(false);
    }

    void OnEnable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnect;
    }

    void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnect;
    }

    void OnClientConnect(ulong clientNum)
    {
        Debug.Log(clientNum);
    }

    public string GetLocalIP()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
        .AddressList.First(
        f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        .ToString();
    }


    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        hostIPText.text = $"IP:{GetLocalIP()}";
        hostWaitingUI.SetActive(true);
    }

    public void SetIP(string ip)
    {
        this.ip = ip;
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
