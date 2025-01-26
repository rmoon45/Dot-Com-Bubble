using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct LineItem
{
    public string name;
    public int cost;

    public LineItem(string name, int cost)
    {
        this.name = name;
        this.cost = cost;
    }

    public override string ToString()
    {
        return $"{name} : ${cost} | ";
    }
}
public struct CostsAndProfits
{
    public List<LineItem> costs;
    public List<LineItem> profits;

    public override string ToString()
    {
        string c = "";
        foreach (var li in costs)
        {
            c += li.ToString();
        }

        string p = "";
        foreach (var li in profits)
        {
            p += li.ToString();
        }

        return $"Costs {c} \n Profits {p}";
    }
}
public class RuleEvaluator
{
    public static CostsAndProfits EvaluateCostsAndProfits(int day, string modules, int[] rules)
    {
        CostsAndProfits costsAndProfits = new CostsAndProfits();

        costsAndProfits.costs = GetCosts(day, modules, rules);
        costsAndProfits.profits = GetProfits(day, modules, rules);

        return costsAndProfits;
    }

    private static List<LineItem> GetCosts(int day, string modules, int[] rules)
    {
        List<LineItem> costs = new List<LineItem>();
        costs.Add(new LineItem("Daily Operations", GetFixedDayCost(day)));
        for (int i = 0; i < rules.Length; i++)
        {
            costs.Add(GetRuleCost(modules, rules[i]));
        }
        return costs;
    }

    private static int GetFixedDayCost(int day)
    {
        return 250 + day * 250;
    }

    private static LineItem GetRuleCost(string modules, int ruleNum)
    {
        string text = "Unkown item";
        int cost = 1000;

        switch (ruleNum)
        {
            case 1:
                text = "it takes 2";
                cost = modules.Count(C => C == 't') == 2 ? 0 : 500;
                break;
            case 2:
                text = "pics bad";
                cost = modules.Count(C => C == 'i') * 250;
                break;
        }

        return new LineItem(text, cost);
    }

    private static List<LineItem> GetProfits(int day, string modules, int[] rules)
    {
        List<LineItem> profits = new List<LineItem>();
        for (int i = 0; i < rules.Length; i++)
        {
            profits.Add(GetRuleProfit(modules, rules[i]));
        }
        return profits;
    }

    private static LineItem GetRuleProfit(string modules, int ruleNum)
    {
        string text = "Unkown item";
        int profit = 1000;

        switch (ruleNum)
        {
            case 1:
                text = "Take a picture";
                profit = modules.Count(C => C == 'i') * 250;
                break;
            case 2:
                text = "text good";
                profit = modules.Count(C => C == 't') * 100;
                break;
        }

        return new LineItem(text, profit);
    }

}
