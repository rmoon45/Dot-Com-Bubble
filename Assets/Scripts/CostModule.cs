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
    public void SetCostsModule() {
        Debug.Log("Setting new day's profits within the UI visually");

        ModuleOption gotImageModuleOption = gameplayerManager.GetComponent<GameplayManager>().GetModuleOption("image");
        imageCostText.text = "-$" + gotImageModuleOption.cost.ToString();

        ModuleOption gotTextModuleOption = gameplayerManager.GetComponent<GameplayManager>().GetModuleOption("text");
        textCostText.text = "-$" + gotTextModuleOption.cost.ToString();
    }
}
