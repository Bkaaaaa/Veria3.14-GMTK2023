using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewsSetUp : MonoBehaviour
{
    public float timerLimit = 5f;
    public float timer = 0f;
    public bool shouldDestroy = true;

    public TextMeshProUGUI newsText;
    public GameObject header;

    public void setNews(string text, bool shouldBeDestroyed)
    {
        newsText.text = text;
        shouldDestroy = shouldBeDestroyed;

        if (!shouldDestroy)
        {
            GetComponent<Animator>().enabled = false;
            header.SetActive(false);
            newsText.margin = newsText.margin - new Vector4(0, newsText.margin.y, 0, 0) + new Vector4(0, 15, 0, 0);
        }
    }

    private void Update()
    {
        if (shouldDestroy)
        {
            timer += Time.deltaTime;
            if (timer >= timerLimit)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void markAsRead()
    {
        Destroy(this.gameObject);
    }
}
