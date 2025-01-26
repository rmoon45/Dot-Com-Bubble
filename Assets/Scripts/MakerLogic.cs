using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MakerLogic : MonoBehaviour
{
    ModuleType[] moduleTypes = new ModuleType[6];
    public List<CostModule> costModules = new List<CostModule>();
    public NewsManager newsManager;
    public RulesManager rulesManager;
    public NetworkedGameManager networkedGameManager;
    public GameObject disableBlocker;
    private bool interactionDisabled = false;

    public void ResetMaker()
    {
        resetModules();
        UnlockModule(1);
        UnlockModule(2);
    }

    public void resetModules()
    {
        foreach (var cm in costModules)
        {
            cm.Reset();
        }
    }

    public void SetModule(int moduleNum, ModuleType type)
    {
        moduleTypes[moduleNum] = type;
        //Debug.Log(modulesToString());
        networkedGameManager.SetModulesRPC(new FixedString128Bytes(modulesToString()));
    }

    public void UnlockModule(int num)
    {
        costModules[num - 1].Unlock();
    }


    public string modulesToString()
    {
        string ret = "";
        for (int i = 0; i < 6; i++)
        {
            ret += $"{i}{moduleTypeToString(moduleTypes[i])}";
        }
        return ret;
    }

    private string moduleTypeToString(ModuleType type)
    {
        if (type == ModuleType.None) return "n";
        if (type == ModuleType.Text) return "t";
        if (type == ModuleType.Image) return "i";
        if (type == ModuleType.Ad) return "a";
        if (type == ModuleType.Chatbot) return "c";

        return "0";
    }

    public void SetNewsForRules(int[] ruleNums)
    {
        string s = "";
        foreach (int ruleNum in ruleNums)
        {
            s += $"{ruleNum}, ";
        }
        Debug.Log(s);
        string[] news = new string[ruleNums.Length];
        for (int i = 0; i < ruleNums.Length; i++)
        {
            news[i] = rulesManager.GetRule(ruleNums[i]).ruleNewsDescription;
        }
        newsManager.SetNews(news);
    }

    public void ClearNews()
    {
        newsManager.ClearNews();
    }

    public void SetInteractionDisabled(bool val)
    {
        interactionDisabled = val;
        disableBlocker.SetActive(val);
    }
}
