using System.Reflection;
using UnityEngine;
using System;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] GameObject investorScreen;
    [SerializeField] GameObject makerScreen;

    [SerializeField] GameObject timeManager;

    [SerializeField] GameObject displayProfitModule;

    [SerializeField] GameObject costModule;

    [SerializeField] int moneyPool;

    // array of module options
    [SerializeField] public ModuleOption[] moduleOptions;

    /// <summary>
    ///  I need this to get that stupid module option from string alone 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ModuleOption GetModuleOption(string name) {
        ModuleOption mo = Array.Find(moduleOptions, moduleOpt => moduleOpt.name == name);

        if (mo == null) {
            Debug.Log("Couldn't find {name} in module options array");
        }
        return mo;
    }


    void Start() {


        // get the scripts from the screen objects
        InvestorLogic investorLogic = investorScreen.GetComponent<InvestorLogic>();

        MakerLogic makerLogic = makerScreen.GetComponent<MakerLogic>();


        // subscribe events
        TimeManager.Instance.OnEndTime += CalculateCostsEndOfDay;
        
        TimeManager.Instance.FireNewDay += CalculateMoneyNewDay;

        
    }

    void CalculateCostsEndOfDay(object sender, EventArgs e) {
        foreach (ModuleOption mo in moduleOptions) {
            if (mo.isSelectedByMaker) {
                moneyPool -= mo.cost;
                moneyPool += mo.profit;
            }
        }
    }

    /// <summary>
    /// sorry this is also resets the isSelectedByMaker thing the in addition to the randominzing costs and profits
    /// </summary>
    void CalculateMoneyNewDay(object sender, EventArgs e) {
        
        foreach (ModuleOption mo in moduleOptions) {

            mo.cost = UnityEngine.Random.Range(1, 10);
            mo.profit = UnityEngine.Random.Range(2, 12);
            mo.isSelectedByMaker = false;
        }

        // Sorry i need this to make the UI go after the calculate money new day stuff T_T
        DisplayProfitOptionsModule displayProfitOptionsModuleScript = displayProfitModule.GetComponent<DisplayProfitOptionsModule>();
        displayProfitOptionsModuleScript.SetProfitsModule();

        CostModule costModuleScript = costModule.GetComponent<CostModule>();
        costModuleScript.SetCostsModule();
    }

    void OnDisable() {
        
        // unsubscribe stuff from events
        TimeManager.Instance.OnEndTime -= CalculateCostsEndOfDay;

    }
}
