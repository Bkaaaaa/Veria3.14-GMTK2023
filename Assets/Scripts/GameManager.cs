using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTimerLimit = 300f;
    public float gameTimer = 0f;

    public TextMeshProUGUI gameTimerText;


    public int evilness { get; private set; } = 40;
    public int happiness { get; private set; } = 40;

    public int healthcare { get; private set; } = 50;
    public int gastronomy { get; private set; } = 50;
    public int conspirations { get; private set; } = 75;

    public int silentness { get; private set; } = 0;
    public int goddess { get; private set; } = 0;

    private int goodAnswer = 0;
    private int halfAnswer = 0;
    private int neutralAnswer = 0;
    private int wtfAnswer = 0;

    private int answered = 0;
    private int notAnswered = 0;

    public float basicSpeed = 4f;
    public float silentImpact = 1.0f;
    public float godImpact = 1.0f;
    public float speedLimit = 0.25f;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
    }

    public void addNotAnswered()
    {
        notAnswered++;
    }

    public void submitAnswer(answerType.answer answerType, QuestionObject qo, PersonObject.influence inf)
    {
        float multiplier = 1;
        string text = "";

        // count answers type
        switch(answerType)
        {
            case global::answerType.answer.good:
                goodAnswer++;
                text = qo.newsGood;
                break;
            case global::answerType.answer.half:
                halfAnswer++;
                multiplier = 0.5f;
                text = qo.newsHalf;
                break;
            case global::answerType.answer.neutral:
                neutralAnswer++;
                multiplier = 0f;
                text = qo.newsNeutral;
                break;
            case global::answerType.answer.wtf:
                wtfAnswer++;
                multiplier = -1f;
                text = qo.newsWTF;
                break;
        }

        // Adjust criteria
        for (int i = 0; i < qo.criterionImpacted.Length; i++) {
            switch (qo.criterionImpacted[i])
            {
                case global::criteria.list.Evilness:
                    evilness += (int)Mathf.Round(qo.amountImpacted[i] * multiplier);
                    break;
                case global::criteria.list.Happiness:
                    happiness += (int)Mathf.Round(qo.amountImpacted[i] * multiplier);
                    break;

                case global::criteria.list.Healthcare:
                    healthcare += (int)Mathf.Round(qo.amountImpacted[i] * multiplier);
                    break;
                case global::criteria.list.gastronomy:
                    gastronomy += (int)Mathf.Round(qo.amountImpacted[i] * multiplier);
                    break;
                case global::criteria.list.conspirations:
                    conspirations += (int)Mathf.Round(qo.amountImpacted[i] * multiplier);
                    break;
            }
        }

        int proba = 0;
        switch(inf)
        {
            case global::PersonObject.influence.inexistant:
                proba = 20;
                break;
            case global::PersonObject.influence.normal:
                proba = 50;
                break;
            case global::PersonObject.influence.popular:
                proba = 95;
                break;
        }

        answered++;

        // Adjust silentness based on answers
        // Adjust goddess based on answers
        calcParameters();

        // News
        if (!text.Equals("")) {
            int value = Random.Range(0, 100);
            if (value < proba)
                NewsManager.instance.newNews(text);
        }
    }

    private void calcParameters()
    {
        silentness = Mathf.RoundToInt(100f * (((float)notAnswered) / (float)(notAnswered + answered + 1)));

        goddess = Mathf.RoundToInt(100f * (((float)goodAnswer) / (float)(answered + 1)));

        //Debug.Log(silentness + " : " + goddess);
    }

    public float calcSpawnDelay()
    {
        float speed = basicSpeed;

        if (answered + notAnswered > 3)
        {
            speed += silentImpact * (float)(silentness - 50) / 100.0f;
            speed -= godImpact * (float)(goddess - 50) / 100.0f;

            //Debug.Log(speed + " : " + (silentImpact * (float)(silentness - 50) / 100.0f) + " - " + (godImpact * (float)(goddess - 50) / 100.0f));
        }

        return Random.Range(speed - speedLimit * speed, speed + speedLimit);
    }

    public int calcScore()
    {
        int score =
            answered * 10 +
            goodAnswer * 100 +
            halfAnswer * 50 +
            wtfAnswer * (-100) +
            ((-silentness) + 100 + goddess) * 10 +
            (-evilness) * 25 +
            happiness * 25;

        return score;
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;

        if (!EndGameMenu.instance.gameObject.activeInHierarchy && gameTimer >= gameTimerLimit)
        {
            Debug.Log("End of game");
            QuestionManager.instance.destroyAll();

            EndGameMenu.instance.gameObject.SetActive(true);

            EndGameMenu.instance.EndGame();
        }
        
        if (gameTimer < gameTimerLimit)
        {
            int hour = Mathf.FloorToInt(gameTimer / (gameTimerLimit / 24));
            int minute = 59 - ( Mathf.RoundToInt(gameTimer / (gameTimerLimit / (24*60))) - hour * 60);

            if (minute < 0)
                minute = 0;

            gameTimerText.text = (23 - hour) + ":" + (minute < 10 ? "0" : "") + minute;
        }
    }
}
