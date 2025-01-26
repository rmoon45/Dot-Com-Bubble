using Unity.Netcode;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using Unity.Networking;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;
using System;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.UI;
public class NetworkedGameManager : NetworkBehaviour
{
    public Lobby lobby;
    public int startingMoney = 10_000;

    public Role role;

    public NetworkVariable<int> money = new NetworkVariable<int>(0);
    private NetworkVariable<int> moneyGained = new NetworkVariable<int>(0);
    private NetworkVariable<int> moneyLost = new NetworkVariable<int>(0);
    public NetworkVariable<int> currentDay = new NetworkVariable<int>(0);


    public TextMeshProUGUI moneyText;
    public GameObject makerCanvas;
    public GameObject investorCanvas;

    public override void OnNetworkSpawn()
    {
        money.OnValueChanged += (int prev, int newVal) =>
        {
            OnChangeMoney(newVal);
        };
    }

    public void StartGame(Role role)
    {
        this.role = role;
        makerCanvas.SetActive(role == Role.Maker);
        investorCanvas.SetActive(role == Role.Investor);
        money.Value = startingMoney;
        moneyLost.Value = 0;
        moneyGained.Value = 0;
    }

    public void OnChangeMoney(int moneyValue)
    {
        moneyText.text = $"Money: ${moneyValue}";
        if (moneyValue <= 0)
        {
            Debug.Log("You lose!");
            CoroutineUtils.ExecuteAfterEndOfFrame(() => { EndGame(); }, this);
        }
    }

    public void onClickMoneyButton()
    {
        int change = role == Role.Maker ? -1000 : 1000;
        AddMoneyRPC(change);
    }

    [Rpc(SendTo.Server)]
    public void AddMoneyRPC(int amount)
    {
        if (amount > 0)
        {
            moneyGained.Value += amount;
        }
        else
        {
            moneyLost.Value += amount * -1;
        }
        money.Value += amount;
    }


    public void EndGame()
    {
        makerCanvas.SetActive(false);
        investorCanvas.SetActive(false);
        lobby.EndGameFromGameManager(currentDay.Value, money.Value, moneyGained.Value, moneyLost.Value);
    }

}
