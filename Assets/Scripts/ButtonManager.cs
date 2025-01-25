using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject makerScreen;
    [SerializeField] private GameObject module;
    [SerializeField] private GameObject imageGameObject;

    [SerializeField] private GameObject textGameObject;
    
    public void InstantiateObjectUnderMaker() {
        // make the game object spawn under the maker screen parent object
        Instantiate(imageGameObject, makerScreen.transform);
    }

    public void InstantiateImageUnderMaker() {
        // should replace the module with this thing probably let's see!
        Instantiate(imageGameObject, new Vector3(module.transform.position.x, module.transform.position.y, 0), transform.rotation, makerScreen.transform);
        module.SetActive(false);
    }

    public void InstntiateTextUnderMaker() {
       Instantiate(textGameObject, new Vector3(module.transform.position.x, module.transform.position.y, 0), transform.rotation, makerScreen.transform);
       module.SetActive(false);
    }
}
