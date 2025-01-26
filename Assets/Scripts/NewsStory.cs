using TMPro;
using UnityEngine;

public class NewsStory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI newsStoryText;

    /// <summary>
    /// Sets the news story text mesh pro text 
    /// </summary>
    /// <param name="text"></param>
    public void SetNewsStoryText(string text) {
        Debug.Log("Setting the news story text");
        newsStoryText.text = text;

    }
}
