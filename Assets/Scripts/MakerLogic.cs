using System.Collections.Generic;
using UnityEngine;

public class MakerLogic : MonoBehaviour
{
    ModuleType[] moduleTypes = new ModuleType[6];
    public List<CostModule> costModules = new List<CostModule>();

    void Start()
    {
        resetModules();
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
        Debug.Log(modulesToString());
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
        return "0";
    }
}
