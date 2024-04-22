using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    public static EndGameMenu instance;

    public TextMeshProUGUI textEndGame;

    public Transform leftContent;
    public ScrollRect leftScrollRect;
    public Transform rightContent;
    public ScrollRect rightScrollRect;

    public float yPos = 1f;
    public float speed = 0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    public AnimationCurve curveShake = new AnimationCurve();

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

        this.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void FixedUpdate()
    {
        yPos -= speed * Time.fixedDeltaTime;

        if (yPos < 0)
            yPos = 1f;

        leftScrollRect.verticalNormalizedPosition = yPos;
        rightScrollRect.verticalNormalizedPosition = yPos;
    }

    public void Grow(RectTransform rt)
    {
        StartCoroutine(CorGrow(rt));
    }

    IEnumerator CorGrow(RectTransform rt)
    {
        float timeShake = 0.35f;
        float timeSpent = 0f;
        float yPos = rt.position.y;

        while (timeSpent < timeShake)
        {
            rt.position = new Vector3(rt.position.x, yPos + curveShake.Evaluate(timeSpent / timeShake) * 4, rt.position.z);
            yield return new WaitForSeconds(0.035f);
            timeSpent += 0.05f;
        }

        rt.position = new Vector3(rt.position.x, yPos, rt.position.z);
    }

    public void EndGame()
    {
        // Score
        int score = GameManager.instance.calcScore();
        scoreText.text = "Score : " + score;
        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
            highscoreText.text = "Highscore : " + score;
        } else
        {
            highscoreText.text = "Highscore : " + PlayerPrefs.GetInt("score");
        }


        for (int i = 0; i < NewsManager.instance.everyNews.Count; i++)
        {
            if (i % 2 == 0)
                NewsManager.instance.newNews(NewsManager.instance.everyNews[i], leftContent, false);
            else
                NewsManager.instance.newNews(NewsManager.instance.everyNews[i], rightContent, false);
        }
        speed = 1f / NewsManager.instance.everyNews.Count;


        string result = "";

        // evilness
        if (GameManager.instance.evilness < 0)
            result += "People are harmless against others.";
        else if (GameManager.instance.evilness < 33)
            result += "People like to hurt others.";
        else if (GameManager.instance.evilness < 67)
            result += "People really like to hurt others. Some countries declared war to their neighbours.";
        else
            result += "People hurt each other. Every country has declared war to at least one other country.";

        result += "\n";

        // happiness
        if (GameManager.instance.happiness < 0)
            result += "People are not happy. They hate their life.";
        else if (GameManager.instance.happiness < 33)
            result += "People tend to smile a few times a day.";
        else if (GameManager.instance.happiness < 67)
            result += "People laugh a lot, in group and even when alone.";
        else
            result += "People are happy. They are satisfied about their life.";

        result += "\n";

        // healthcare
        if (GameManager.instance.healthcare < 0)
            result += "People are sick. They do not care about their hygiene.";
        else if (GameManager.instance.healthcare < 33)
            result += "People get easily sick. 50% of them stay in bed once a week.";
        else if (GameManager.instance.healthcare < 67)
            result += "People are quite healthy. They listen to doctors.";
        else
            result += "People are super healthy. 80% of them are not sick even once in a year.";

        result += "\n";

        // gastronomy
        if (GameManager.instance.gastronomy < 0)
            result += "People do not pay attention to what they eat. Some restaurant are full of rats.";
        else if (GameManager.instance.gastronomy < 33)
            result += "People care about the hygiene of their food. But do not care about what it is.";
        else if (GameManager.instance.gastronomy < 67)
            result += "People enjoy eating and pay attention to what they consume";
        else
            result += "People love eating and are eating correctly. There is no more obesity.";

        result += "\n";

        // conspirations
        if (GameManager.instance.conspirations < 0)
            result += "People do not trust anyone, especially the government.";
        else if (GameManager.instance.conspirations < 33)
            result += "People are suspicious with their friends but do not trust at all the government.";
        else if (GameManager.instance.conspirations < 67)
            result += "People believe their friends but are suspicious at the government.";
        else
            result += "People believe in what they are told.";

        textEndGame.text = result;
    }
}
