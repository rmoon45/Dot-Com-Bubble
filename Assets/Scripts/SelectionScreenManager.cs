using UnityEngine;

public class SelectionScreenManager : MonoBehaviour
{

    [SerializeField] private GameObject makerScreen;

    [SerializeField] private GameObject investorScreen;

    [SerializeField] private GameObject timeManager;


    public void ActivateMakerScreen() {    
        makerScreen.SetActive(true);
        timeManager.SetActive(true);
        gameObject.SetActive(false);

    }

    public void ActivateInvestorScreen() {
        investorScreen.SetActive(true);
        timeManager.SetActive(true);
        gameObject.SetActive(false);
    }
}
