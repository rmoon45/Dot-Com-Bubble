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
using System.Collections;
using UnityEngine.UI;

public class Lobby : NetworkBehaviour
{
    public TextMeshProUGUI hostIPText;
    public GameObject hostWaitingUI;
    public GameObject RoleSelectUI;

    private UnityTransport transport => (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
    private string ip;

    [Serializable]
    public enum Role
    {
        None,
        Maker,
        Investor
    }

    public NetworkVariable<Role> hostRole = new NetworkVariable<Role>(Role.None);
    public NetworkVariable<Role> clientRole = new NetworkVariable<Role>(Role.None);

    public TextMeshProUGUI hostRoleText;
    public TextMeshProUGUI clientRoleText;
    public Button startButton;

    public override void OnNetworkSpawn()
    {
        hostRole.OnValueChanged += (Role prev, Role newRole) =>
        {
            UpdateHostRoleText(newRole);
            SetStartState();
        };
        clientRole.OnValueChanged += (Role prev, Role newRole) =>
        {
            UpdateClientRoleText(newRole);
            SetStartState();
        };

    }

    void OnEnable()
    {
        StartCoroutine(SubscribeToNetworkManagerEvents());

    }

    IEnumerator SubscribeToNetworkManagerEvents()
    {
        yield return new WaitUntil(() => NetworkManager.Singleton);
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnect;
    }

    void OnDisable()
    {
        if (NetworkManager.Singleton)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnect;
        }
    }

    void OnClientConnect(ulong clientNum)
    {
        Debug.Log(clientNum);
        if (clientNum == 1)
        {
            ShowRoleWindow();
        }
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
        Debug.Log(ip);
        if (ip != null && ip != "")
        {
            transport.ConnectionData.Address = ip;
            Debug.Log(ip);
            transport.SetConnectionData(ip, 7777);
        }
        NetworkManager.Singleton.StartClient();
    }

    void ShowRoleWindow()
    {
        hostWaitingUI.SetActive(false);
        RoleSelectUI.SetActive(true);
    }

    public void SelectInvestor()
    {
        bool isHost = NetworkManager.Singleton.IsHost;
        NetworkVariable<Role> myRoleVar = isHost ? hostRole : clientRole;
        NetworkVariable<Role> otherRoleVar = isHost ? clientRole : hostRole;

        if (myRoleVar.Value == Role.Investor)
        {
            SetRoleRPC(isHost, Role.None);
        }
        else if (otherRoleVar.Value != Role.Investor)
        {
            SetRoleRPC(isHost, Role.Investor);
        }
        else
        {
            Debug.LogError("Invalid pick");
        }
    }

    public void SelectMaker()
    {
        bool isHost = NetworkManager.Singleton.IsHost;
        NetworkVariable<Role> myRoleVar = isHost ? hostRole : clientRole;
        NetworkVariable<Role> otherRoleVar = isHost ? clientRole : hostRole;


        if (myRoleVar.Value == Role.Maker)
        {
            SetRoleRPC(isHost, Role.None);
        }
        else if (otherRoleVar.Value != Role.Maker)
        {
            SetRoleRPC(isHost, Role.Maker);
        }
        else
        {
            Debug.LogError("Invalid pick");
        }
    }

    [Rpc(SendTo.Server)]
    public void SetRoleRPC(bool isHost, Role value)
    {
        Debug.Log("setting role " + value + " " + isHost);
        var netvar = isHost ? hostRole : clientRole;
        netvar.Value = value;
    }

    public void UpdateHostRoleText(Role role)
    {
        hostRoleText.text = $"Player 1: {role.ToString()}";
    }


    public void UpdateClientRoleText(Role role)
    {
        clientRoleText.text = $"Player 2: {role.ToString()}";
    }

    public void SetStartState()
    {
        if (hostRole.Value != Role.None && clientRole.Value != Role.None)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    public void OnClickStart()
    {
        if (hostRole.Value == Role.None || clientRole.Value == Role.None)
        {
            Debug.LogError("Select Roles!");
            return;
        }

        StartGame();
    }

    public void StartGame()
    {
        RoleSelectUI.SetActive(false);
    }

    public void ResetRoles()
    {
        SetRoleRPC(true, Role.None);
        SetRoleRPC(false, Role.None);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
