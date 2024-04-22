using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(QuestionSetUp))]
public class QuestionMechanic : MonoBehaviour
{

    public int questionIndex = 0;

    // answer type
    public answerType.answer[] answersTypeShuffled = new answerType.answer[4];

    // timer
    public float timeLimit = 8f;
    public float timeLimitBorder = 0.2f;

    public GameObject nqisv;
    public GameObject po;
    public QuestionObject qo;

    public AnimationCurve curveShake = new AnimationCurve();

    private bool isDestroying = false;

    private void Start()
    {
        timeLimit = Random.Range(timeLimit - timeLimitBorder * timeLimit, timeLimit + timeLimitBorder * timeLimit);

        GetComponent<QuestionSetUp>().setTimer(timeLimit);
    }

    public void submit(int index)
    {
        if (!isDestroying)
            GameManager.instance.submitAnswer(
                answersTypeShuffled[index],
                qo,
                po.GetComponent<PersonSetUp>().po.influenceText);

        destroyCorrectly();
    }

    public void submitRandom()
    {
        submit(Random.Range(0, answersTypeShuffled.Length));
    }

    public void submitNotAnswered()
    {
        if (!isDestroying)
            GameManager.instance.addNotAnswered();

        destroyCorrectly();
    }

    public void destroyCorrectly()
    {
        if (!isDestroying)
        {
            isDestroying = true;
            QuestionManager.instance.removeFromCurrent(this.gameObject.name);
            if (this.GetComponent<QuestionSetUp>().isActive)
            {
                this.GetComponent<QuestionSetUp>().hide();
                QuestionManager.instance.selectNext();
            }

            Destroy(nqisv);
            Destroy(po, 0.2f);
            Destroy(this.gameObject, 0.2f);
        }
    }

    public void setInScrollView(GameObject _nqisv)
    {
        nqisv = _nqisv;
    }

    public void setPersonUI(GameObject _po)
    {
        po = _po;
        setUpQuestion();
    }

    public void setUpQuestion()
    {
        PersonObject p = po.GetComponent<PersonSetUp>().po;

        List<int> possibleQuestions = new List<int>();

        for (int i = 0; i < p.questions.Length; i++)
        {
            if (p.asked[i] == false)
            {
                possibleQuestions.Add(i);
            }
        }

        // No question possible
        if (possibleQuestions.Count == 0)
        {
            for (int i = 0; i < p.questions.Length; i++)
            {
                p.asked[i] = false;
                possibleQuestions.Add(i);
            }
        }

        // Choose a new question
        int randIndex = Random.Range(0, possibleQuestions.Count);
        qo = p.questions[randIndex];

        string[] answersShuffled = new string[4];

        List<int> list = new List<int>();
        list.AddRange(new int[] { 0, 1, 2, 3 });

        int randPos = Random.Range(0, list.Count);
        answersTypeShuffled[list[randPos]] = answerType.answer.good;
        answersShuffled[list[randPos]] = qo.answerGood;
        list.RemoveAt(randPos);

        randPos = Random.Range(0, list.Count);
        answersTypeShuffled[list[randPos]] = answerType.answer.half;
        answersShuffled[list[randPos]] = qo.answerHalf;
        list.RemoveAt(randPos);

        randPos = Random.Range(0, list.Count);
        answersTypeShuffled[list[randPos]] = answerType.answer.neutral;
        answersShuffled[list[randPos]] = qo.answerNeutral;
        list.RemoveAt(randPos);

        randPos = Random.Range(0, list.Count);
        answersTypeShuffled[list[randPos]] = answerType.answer.wtf;
        answersShuffled[list[randPos]] = qo.answerWTF;
        list.RemoveAt(randPos);

        GetComponent<QuestionSetUp>().setUpUI(qo.question, answersShuffled[0], answersShuffled[1], answersShuffled[2], answersShuffled[3]);
    }

    public void buttonOnHover(RectTransform rt)
    {
        StartCoroutine(Hover(rt));
    }

    IEnumerator Hover(RectTransform rt)
    {
        float timeShake = 0.35f;
        float timeSpent = 0f;
        float yPos = rt.position.y;

        while (timeSpent < timeShake)
        {
            rt.position = new Vector3(rt.position.x, yPos + curveShake.Evaluate(timeSpent/timeShake) * 20, rt.position.z);
            yield return new WaitForSeconds(0.035f);
            timeSpent += 0.05f;
        }

        rt.position = new Vector3(rt.position.x, yPos, rt.position.z);
    }
}
