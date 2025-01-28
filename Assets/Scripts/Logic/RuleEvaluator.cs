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
            costs.Add(GetRuleCost(modules, rules[i], day));
        }
        return costs;
    }

    private static int GetFixedDayCost(int day)
    {
        return 250 + day * 250;
    }

    private static LineItem GetRuleCost(string modules, int ruleNum, int day)
    {
        string text = "Unknown item";
        int cost = 1000;

        int numNothing = modules.Count(C => C == 'n');
        int numtext = modules.Count(C => C == 't');
        int numimages = modules.Count(C => C == 'i');
        int numads = modules.Count(C => C == 'a');
        int numchatbot = modules.Count(C => C == 'c');

        switch (ruleNum)
        {
            case 1:
                text = "Glorp Pictures";
                cost = numimages * 250;
                break;

            case 2:
                text = "Yapping";
                cost = day == 7 ? 0 : (numads + numtext) > 0 ? 750 : 0;
                break;

            case 3:
                text = "Three of a Kind";
                cost = numNothing == 3 || numtext == 3 || numimages == 3 || numads == 3 || numchatbot == 3 ? 200 : 0;
                break;

            case 4:
                text = "Poor Coverage";
                cost = numimages == 0 || numtext == 0 ? 750 : 0;
                break;

            case 5:
                text = "Buggy";
                cost = day < 5 ? numchatbot == 0 ? 1000 : 0 : numchatbot < 3 ? 1000 : 0;
                break;

            case 6:
                text = "Loves me not";
                cost = numNothing == 6 ? 0 : 200;
                break;

            case 7:
                text = "Stop yapping";
                cost = numchatbot * 100;
                break;

            case 8:
                text = "Mr. Director";
                cost = modules.ToCharArray()[3] == 'i' ? 200 : 0;
                break;

            case 9:
                text = "Deadly Laser";
                cost = numNothing * 500;
                break;

            case 10:
                text = "Look Away";
                cost = numimages * 500;
                break;

        }

        return new LineItem(text, cost);
    }

    private static List<LineItem> GetProfits(int day, string modules, int[] rules)
    {
        List<LineItem> profits = new List<LineItem>();
        for (int i = 0; i < rules.Length; i++)
        {
            profits.Add(GetRuleProfit(modules, rules[i], day));
        }
        return profits;
    }

    private static LineItem GetRuleProfit(string modules, int ruleNum, int day)
    {
        string text = "Unknown item";
        int cost = 1000;

        int numNothing = modules.Count(C => C == 'n');
        int numtext = modules.Count(C => C == 't');
        int numimages = modules.Count(C => C == 'i');
        int numads = modules.Count(C => C == 'a');
        int numchatbot = modules.Count(C => C == 'c');

        switch (ruleNum)
        {
            case 1:
                text = "Glorp Text";
                cost = numtext * 100;
                break;

            case 2:
                text = "Preach";
                cost = numads == 2 ? 500 : 0;
                break;

            case 3:
                text = "Special Snowflake";
                cost = numtext == 1 && numimages == 1 && numads == 1 && numchatbot == 1 ? 1000 : 0;
                break;

            case 4:
                text = "Search Party";
                cost = numtext == 1 && numimages == 1 ? 1000 : 0;
                break;

            case 5:
                text = "Robot Uprising";
                cost = numchatbot > 2 ? 1000 : 0;
                break;

            case 6:
                text = "Loves me";
                cost = numNothing == 6 ? 4000 : 0;
                break;

            case 7:
                text = "Sponsored Content";
                cost = numads == 6 ? 300 : 0;
                break;

            case 8:
                text = "Front Page";
                cost = modules.ToCharArray()[1] == 'i' ? 200 : 0;
                break;

            case 9:
                text = "Fight back!";
                cost = (6 - numNothing) * 500;
                break;

            case 10:
                text = "Coming soon";
                cost = numads * 100;
                break;

        }

        return new LineItem(text, cost);

    }

}
