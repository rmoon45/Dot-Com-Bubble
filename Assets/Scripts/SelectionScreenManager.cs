using UnityEngine;

public class SelectionScreenManager : MonoBehaviour
{

    [SerializeField] private GameObject makerScreen;

    [SerializeField] private GameObject investorScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMakerScreen() {    
        makerScreen.SetActive(true);
        gameObject.SetActive(false);

    }

    public void ActivateInvestorScreen() {
        investorScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
