using TMPro;
using UnityEngine;
using System;

/// <summary>
/// this would affect the UI stuff basically for this module
/// </summary>
public class DisplayProfitOptionsModule : MonoBehaviour
{
    [SerializeField] GameObject gameplayerManager;

    [SerializeField] GameObject timeManager;

    [SerializeField] TextMeshProUGUI imageProfitText;

    [SerializeField] TextMeshProUGUI textProfitText;

    void OnEnable() {
        TimeManager timeManagerScript = timeManager.GetComponent<TimeManager>();
        // timeManagerScript.FireNewDay += SetProfitsModule;
    }

    public void SetProfitsModule() {
        Debug.Log("Setting new day's profits within the UI visually");

        ModuleOption gotImageModuleOption = gameplayerManager.GetComponent<GameplayManager>().GetModuleOption("image");
        imageProfitText.text = "+$" + gotImageModuleOption.profit.ToString();

        ModuleOption gotTextModuleOption = gameplayerManager.GetComponent<GameplayManager>().GetModuleOption("text");
        textProfitText.text = "+$" + gotTextModuleOption.profit.ToString();
    }

    void OnDisable() {
        // TimeManager timeManagerScript = timeManager.GetComponent<TimeManager>();
        // timeManagerScript.FireNewDay -= SetProfitsModule;
    }
}
