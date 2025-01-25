using UnityEngine;
using TMPro;

public class CostModule : MonoBehaviour
{
    [SerializeField] GameObject gameplayerManager;

    [SerializeField] TextMeshProUGUI imageCostText;

    [SerializeField] TextMeshProUGUI textCostText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame

    /// <summary>
    /// i'm sorry god for this. i need for the UI i swear to you
    /// </summary>
    public void SetCostsModule() {
        

        ModuleOption gotImageModuleOption = gameplayerManager.GetComponent<GameplayManager>().GetModuleOption("image");
        imageCostText.text = "-$" + gotImageModuleOption.cost.ToString();

        ModuleOption gotTextModuleOption = gameplayerManager.GetComponent<GameplayManager>().GetModuleOption("text");
        textCostText.text = "-$" + gotTextModuleOption.cost.ToString();
    }
}
