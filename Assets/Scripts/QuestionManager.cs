using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager instance;

    public GameObject question;
    public GameObject questionInScrollView;
    public GameObject person;

    public float questionDelay = 6f;
    public float timeSinceLast = 0f;
    private bool shouldContinue = true;

    public int nbQuestions = 0;

    public Transform parentScrollView;
    public Transform canvas;

    private List<QuestionSetUp> currentQuestions = new List<QuestionSetUp>();

    public GameObject threeWaitingDots;

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

    private void Start()
    {
        questionDelay = GameManager.instance.calcSpawnDelay();
    }

    public void CreateNewQuestion()
    {
        GameObject nqisv = Instantiate(questionInScrollView);
        nqisv.transform.SetParent(parentScrollView, false);
        GameObject nq = Instantiate(question);
        nq.transform.SetParent(canvas, false);
        GameObject p = Instantiate(person);
        p.transform.SetParent(canvas, false);

        nq.name = "Question n°" + nbQuestions.ToString();
        nbQuestions++;

        PersonObject po = PersonCreator.instance.getRandomPerson();

        p.GetComponent<PersonSetUp>().setUpUi(po);

        nqisv.GetComponent<OpenQuestion>().setQuestion(nq);
        nqisv.GetComponent<OpenQuestion>().setPersonSprite(po.sprite, po.firstnameText);
        nq.GetComponent<QuestionMechanic>().setInScrollView(nqisv);
        nq.GetComponent<QuestionMechanic>().setPersonUI(p);

        currentQuestions.Add(nq.GetComponent<QuestionSetUp>());
        currentQuestions[currentQuestions.Count - 1].setPersonSprite(po.sprite);

        if (currentQuestions.Count > 1)
            currentQuestions[currentQuestions.Count - 1].hide();
        else
        {
            threeWaitingDots.SetActive(false);
            currentQuestions[0].show();
        }

        Debug.Log("size(in): " + currentQuestions.Count);
    }

    private void Update()
    {
        if (shouldContinue)
        {
            timeSinceLast += Time.deltaTime;
            if (timeSinceLast >= questionDelay)
            {
                this.GetComponent<AudioSource>().Play();
                questionDelay = GameManager.instance.calcSpawnDelay();
                timeSinceLast = 0f;
                CreateNewQuestion();
            }
        }
    }

    public void removeFromCurrent(string name)
    {
        Debug.Log("size(out): " + currentQuestions.Count);

        int index = 0;
        for(int i = 0; i < currentQuestions.Count; i++)
        {
            if (currentQuestions[i].gameObject.name == name)
            {
                index = i;
            }
        }

        if (index > currentQuestions.Count - 1)
        {
            Debug.Log(index);
        }

        currentQuestions.RemoveAt(index);

        if (currentQuestions.Count == 0)
        {
            threeWaitingDots.SetActive(true);
        }
    }

    public void hideAll()
    {
        for(int i = 0; i < currentQuestions.Count; i++)
        {
            currentQuestions[i].hide();
        }
    }

    public void destroyAll()
    {
        while(currentQuestions.Count > 0)
        {
            currentQuestions[0].GetComponent<QuestionMechanic>().destroyCorrectly();
        }
        shouldContinue = false;
    }

    public void selectNext()
    {
        if (currentQuestions.Count > 0)
            currentQuestions[0].show();
    }
}
