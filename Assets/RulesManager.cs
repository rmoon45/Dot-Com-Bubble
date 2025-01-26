using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class RulesManager : NetworkBehaviour
{
    [SerializeField] private List<Rule> rules = new List<Rule>();
    private static System.Random rng = new System.Random();

    public Rule GetRule(int ruleId)
    {
        return rules.Where(r => r.ruleId == ruleId).FirstOrDefault();
    }


    public string SelectRandomRules(int day)
    {
        return getRandomRules(GetNumRules(day), GetMaxDifficulty(day));
    }

    private static int GetNumRules(int day)
    {
        return Mathf.FloorToInt((day + 1) / 2.0f);
    }

    private static int GetMaxDifficulty(int day)
    {
        return Mathf.FloorToInt((day + 1) / 2.0f);
    }

    private string getRandomRules(int numRules, int maxDifficulty)
    {
        string selectedRules = "";
        int numSelected = 0;
        Shuffle(rules);
        int i = 0;
        while (numSelected < numRules)
        {
            Rule r = rules[i];
            {
                if (r.difficulty <= maxDifficulty)
                {
                    var currid = r.ruleId.ToString();
                    if (currid.Length == 1)
                    {
                        currid = "0" + currid;
                    }
                    selectedRules = selectedRules + currid;
                    numSelected++;
                }
            }
            i++;
            if (i == rules.Count)
            {
                Debug.LogError("Could not return enough rules!");
                return selectedRules;
            }
        }
        //  Debug.Log(selectedRules);
        return selectedRules;
    }

    private static void Shuffle(List<Rule> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Rule value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
