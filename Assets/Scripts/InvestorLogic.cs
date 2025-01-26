using TMPro;
using UnityEngine;

public class InvestorLogic : MonoBehaviour
{
    public TextMeshProUGUI unlockText;

    private int numModulesUnlocked;
    public NetworkedGameManager networkedGameManager;

    public void ResetInvestor()
    {
        Debug.Log("here");
        numModulesUnlocked = 2;
        SetText(3);
    }


    public void OnClickUnlock()
    {
        int cost = getCostForModule(numModulesUnlocked);
        if (cost == -1) return;

        numModulesUnlocked++;
        networkedGameManager.AddMoneyRPC(cost * -1);
        networkedGameManager.setNumModulesRPC(numModulesUnlocked);
        SetText(numModulesUnlocked + 1);
    }

    private void SetText(int nextModule)
    {
        if (nextModule > 6)
        {
            unlockText.text = "All Modules Unlocked";
            return;
        }
        unlockText.text = $"Buy Module ${nextModule} ${getCostForModule(nextModule)}";
    }

    public int getCostForModule(int moduleNum)
    {
        if (moduleNum > 6) return -1;
        return 1000 * (int)Mathf.Pow(2, moduleNum - 2);
    }


}
