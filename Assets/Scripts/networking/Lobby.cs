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
    public GameObject hostClientButtons;
    public TextMeshProUGUI hostIPText;
    public GameObject hostWaitingUI;
    public GameObject RoleSelectUI;

    private UnityTransport transport => (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
    private string ip;

    public NetworkVariable<Role> hostRole = new NetworkVariable<Role>(Role.None);
    public NetworkVariable<Role> clientRole = new NetworkVariable<Role>(Role.None);

    public TextMeshProUGUI hostRoleText;
    public TextMeshProUGUI clientRoleText;
    public Button startButton;

    public GameObject gameWindow;
    public TextMeshProUGUI playerText;

    public NetworkedGameManager networkedGameManager;

    public GameObject endGameScreen;
    public TextMeshProUGUI DaysSurvivedText;
    public TextMeshProUGUI MoneyMadeText;
    public TextMeshProUGUI MoneyLostText;

    // public delegate void OnStartGame();
    // public static OnStartGame onStartGame;


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
        //   onStartGame += () => StartGame();

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
        transport.SetConnectionData(GetLocalIP(), 7777);
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
            // transport.ConnectionData.Address = ip;
            //Debug.Log(ip);
            transport.SetConnectionData(ip, 7777);
        }
        NetworkManager.Singleton.StartClient();
    }

    void ShowRoleWindow()
    {
        hostClientButtons.SetActive(false);
        SetStartState();
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

        StartGameRPC();
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void StartGameRPC()
    {
        StartGame();
    }

    private void StartGame()
    {
        RoleSelectUI.SetActive(false);
        gameWindow.SetActive(true);
        Debug.Log("Starting Game");
        bool isHost = NetworkManager.Singleton.IsHost;

        string playernum = isHost ? "1" : "2";
        Role role = isHost ? hostRole.Value : clientRole.Value;
        playerText.text = $"You are in the game player {playernum}. Your role is {role.ToString()}";
        networkedGameManager.StartGame(role);
    }

    public void EndGameFromGameManager(int daysSurvived, int moneyTotal, int moneyMade, int moneyLost)
    {
        EndGameRPC(daysSurvived, moneyTotal, moneyMade, moneyLost);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void EndGameRPC(int daysSurvived, int moneyTotal, int moneyMade, int moneyLost)
    {
        EndGame(daysSurvived, moneyTotal, moneyMade, moneyLost);
    }

    private void EndGame(int daysSurvived, int moneyTotal, int moneyMade, int moneyLost)
    {
        DaysSurvivedText.text = $"Days Survived: {daysSurvived}";
        MoneyMadeText.text = $"Money Made: ${moneyMade}";
        MoneyLostText.text = $"Money Lost: ${moneyLost}";
        gameWindow.SetActive(false);
        endGameScreen.SetActive(true);
    }

    private void ResetRoles()
    {
        SetRoleRPC(true, Role.None);
        SetRoleRPC(false, Role.None);
    }

    public void CloseEndGameScreen()
    {
        ResetRoles();
        endGameScreen.SetActive(false);
        RoleSelectUI.SetActive(true);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
