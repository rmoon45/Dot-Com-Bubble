using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject makerScreen;
    [SerializeField] private GameObject imageGameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateObjectUnderMaker() {
        // make the game object spawn under the maker screen parent object
        Instantiate(imageGameObject, makerScreen.transform);
    }
}
