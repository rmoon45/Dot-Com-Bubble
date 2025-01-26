using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CostModule : MonoBehaviour
{
    public int moduleId;
    public ModuleType moduleType;
    public bool isLocked;
    public List<ModuleButton> buttons;

    public MakerLogic makerLogic;
    public bool unlocked;
    public GameObject lockedObj;

    public void Reset()
    {
        SetType(ModuleType.None);
        unlocked = false;
        lockedObj.SetActive(true);
    }

    public void Unlock()
    {
        unlocked = true;
        lockedObj.SetActive(false);
    }

    public void SetType(ModuleType type)
    {
        moduleType = type;
        makerLogic.SetModule(moduleId, type);
        buttons.ForEach(button => button.SetSelected(button.moduleType == moduleType));
    }



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
    public void SetCostsModule()
    {


        ModuleOption gotImageModuleOption = gameplayerManager.GetComponent<GameplayManager>().GetModuleOption("image");
        imageCostText.text = "-$" + gotImageModuleOption.cost.ToString();

        ModuleOption gotTextModuleOption = gameplayerManager.GetComponent<GameplayManager>().GetModuleOption("text");
        textCostText.text = "-$" + gotTextModuleOption.cost.ToString();
    }
}
