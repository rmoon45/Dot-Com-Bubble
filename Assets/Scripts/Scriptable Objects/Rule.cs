using UnityEngine;

[CreateAssetMenu(fileName = "Rule", menuName = "Scriptable Objects/Rule")]
public class Rule : ScriptableObject
{
    public int ruleId;
    public string ruleName;
    public string ruleNewsDescription;
    public string ruleManualDescription;

    public int difficulty;
}
