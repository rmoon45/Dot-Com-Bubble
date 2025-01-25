using UnityEngine;
using TMPro;

public class MoneyPool : MonoBehaviour
{
    [SerializeField] GameObject gameplayManager;

    [SerializeField] TextMeshProUGUI moneyPoolText;

    GameplayManager gameplayerManagerScript;

    void Start() {
        gameplayerManagerScript = gameplayerManagerScript.GetComponent<GameplayManager>();
    }
    
    void UpdateMoneyPoolText() {
        
        moneyPoolText.text = gameplayerManagerScript.moneyPool.ToString();
    }
    
}
