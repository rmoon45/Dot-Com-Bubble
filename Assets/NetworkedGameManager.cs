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
using Unity.Collections;
public class NetworkedGameManager : NetworkBehaviour
{
    public Lobby lobby;
    public int startingMoney = 10_000;
    public int numDays = 5;
    public float dayLength = 10;

    public Role role;

    public NetworkVariable<bool> inGame = new NetworkVariable<bool>(false);
    public NetworkVariable<int> money = new NetworkVariable<int>(0);
    private NetworkVariable<int> moneyGained = new NetworkVariable<int>(0);
    private NetworkVariable<int> moneyLost = new NetworkVariable<int>(0);
    public NetworkVariable<int> currentDay = new NetworkVariable<int>(1);
    public NetworkVariable<float> dayTimer = new NetworkVariable<float>(0);
    public NetworkVariable<bool> timerActive = new NetworkVariable<bool>(false);

    private NetworkVariable<FixedString128Bytes> currSelectedRules = new NetworkVariable<FixedString128Bytes>();


    public TextMeshProUGUI moneyText;
    public GameObject makerCanvas;
    public GameObject investorCanvas;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI dayText;

    public RulesManager rulesManager;
    public MakerLogic makerLogic;

    public event EventHandler OnEndDay;
    // public event EventHandler FireNewDay;


    public override void OnNetworkSpawn()
    {
        money.OnValueChanged += (int prev, int newVal) =>
        {
            OnChangeMoney(newVal);
        };
        currentDay.OnValueChanged += (_, newval) => { Debug.Log("day " + newval); };
        currSelectedRules.OnValueChanged += (_, newval) => { HandleRuleUpdates(newval); };
    }

    public void StartGame(Role role)
    {
        this.role = role;
        makerCanvas.SetActive(role == Role.Maker);
        investorCanvas.SetActive(role == Role.Investor);
        if (IsHost)
        {
            money.Value = startingMoney;
            moneyLost.Value = 0;
            moneyGained.Value = 0;
            inGame.Value = true;

            StartNextDay(1);
        }
    }

    private void Update()
    {
        if (!IsHost || !inGame.Value) return;
        TickTimer();
    }

    private void TickTimer()
    {
        if (!timerActive.Value) return;
        // if (timer == timerLength)
        // {
        //     // fire for every new subsequent day
        //     FireNewDay?.Invoke(this, EventArgs.Empty);
        // }
        if (dayTimer.Value > 0)
        {
            dayTimer.Value -= Time.deltaTime;
            UpdateTimerTextRPC(dayTimer.Value);
        }
        else
        {
            StartCoroutine(EndDayCoroutine());
        }
    }


    private IEnumerator EndDayCoroutine()
    {
        //disable interaction ?
        Debug.Log("end of day");
        timerActive.Value = false;
        CleanupDayRPC();
        yield return new WaitForSeconds(1);

        AddMoneyRPC(CalculateCosts() * -1);
        yield return new WaitForSeconds(1);

        AddMoneyRPC(CalculateProfit());
        yield return new WaitForSeconds(1);

        GoToNextDay();
    }

    private void GoToNextDay()
    {
        dayTimer.Value = dayLength;
        int nextDay = currentDay.Value + 1;
        OnEndDay?.Invoke(this, EventArgs.Empty);
        if (role == Role.Maker)
        {
        }
        if (nextDay > numDays)
        {
            EndGameRPC(true);
        }
        else
        {
            StartNextDay(nextDay);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void CleanupDayRPC()
    {
        if (role == Role.Maker)
        {
            makerLogic.ClearNews();
        }
    }

    private void StartNextDay(int dayNum)
    {
        string rules = rulesManager.SelectRandomRules(dayNum);
        Debug.Log("rules " + rules);
        currSelectedRules.Value = new FixedString128Bytes(rules);

        currentDay.Value = dayNum;
        dayTimer.Value = dayLength;
        timerActive.Value = true;

        UpdateTimerTextRPC(dayLength);
        ChangeDayTextRPC(dayNum);
        Debug.Log("Starting next day");
    }

    private void HandleRuleUpdates(FixedString128Bytes rules)
    {
        if (role == Role.Maker)
        {
            makerLogic.ClearNews();
            makerLogic.SetNewsForRules(decodeRules(rules.ToString()));
        }
    }

    private int[] decodeRules(string rules)
    {
        int length = Mathf.FloorToInt(rules.Length / 2.0f);
        int[] r = new int[length];
        for (int i = 0; i < length; i++)
        {
            string s = rules.Substring(2 * i, 2);
            r[i] = int.Parse(s);
        }
        Debug.Log(r);
        return r;
    }


    private int CalculateCosts()
    {
        //use rules and modules
        return 1000;
    }

    private int CalculateProfit()
    {
        //use rules and modules
        return 1000;
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void UpdateTimerTextRPC(float time)
    {
        if (time < 0) time = 0;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void ChangeDayTextRPC(int day)
    {
        dayText.text = $"Day {day}";
    }


    public void OnChangeMoney(int moneyValue)
    {
        moneyText.text = $"${moneyValue}";
        if (moneyValue <= 0)
        {
            StopAllCoroutines();
            Debug.Log("You lose!");
            CoroutineUtils.ExecuteAfterEndOfFrame(() => { EndGameRPC(false); }, this);
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

    [Rpc(SendTo.ClientsAndHost)]
    public void EndGameRPC(bool win)
    {
        if (IsHost)
        {
            inGame.Value = false;
            lobby.EndGameFromGameManager(win, currentDay.Value, money.Value, moneyGained.Value, moneyLost.Value);
        }
        makerCanvas.SetActive(false);
        investorCanvas.SetActive(false);
    }

}
