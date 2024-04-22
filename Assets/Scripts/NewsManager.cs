using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public static NewsManager instance;

    public GameObject newsPrefab;
    public Transform scrollViewContent;

    public List<string> everyNews = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void newNews(string text)
    {
        newNews(text, scrollViewContent, true);
        everyNews.Add(text);
    }

    public void newNews(string text, Transform parent, bool shouldDestroy)
    {
        GameObject n = Instantiate(newsPrefab);
        n.transform.SetParent(parent, false);

        n.GetComponent<NewsSetUp>().setNews(text, shouldDestroy);
    }
}
