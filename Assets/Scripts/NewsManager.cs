using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    [SerializeField] GameObject newsStory;

    [SerializeField] GameObject contentObject;

    // create a list of gameobjects
    List<GameObject> newsStoryInstantiatedList;

    void Start()
    {
        newsStoryInstantiatedList = new List<GameObject>();
    }

    public void ClearNews()
    {
        foreach (GameObject ns in newsStoryInstantiatedList)
        {
            Destroy(ns);
        }
        newsStoryInstantiatedList.Clear();
    }

    /// <summary>
    /// Should take in array of strings and create the news system
    /// </summary>
    /// <param name="newsLines"></param>
    public void SetNews(string[] newsLines)
    {
        // clear out the prior newsStory stuff



        // loop thru the array
        foreach (string newL in newsLines)
        {
            // should add a child under the Content parent
            GameObject instantiatedNewsStory = Instantiate(newsStory, contentObject.transform);

            // this should add it to a dynamic list of NewsStory (hopefully)
            newsStoryInstantiatedList.Add(instantiatedNewsStory);
            // call the function to set the newsStoryText yay
            instantiatedNewsStory.GetComponent<NewsStory>().SetNewsStoryText(newL);
        }
    }
}
