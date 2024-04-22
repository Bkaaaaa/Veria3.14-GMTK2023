using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSetUp : MonoBehaviour
{
    // Question UI
    public TextMeshProUGUI questionUI;
    public TextMeshProUGUI[] questions = new TextMeshProUGUI[4];

    public Gradient gradient = new Gradient();
    public Image timerImage;
    public float timeLimit;
    public float time;

    public bool isActive;

    public Image person;

    public void setUpUI(string q, string a1, string a2, string a3, string a4)
    {
        questionUI.text = q;

        questions[0].text = a1;
        questions[1].text = a2;
        questions[2].text = a3;
        questions[3].text = a4;
    }

    public void setTimer(float timer)
    {
        timeLimit = timer;
        time = 0f;
    }

    private void Update()
    {
        // Update time limit
        if (time <= timeLimit)
        {
            time += Time.deltaTime;
            timerImage.fillAmount = time / timeLimit;
            timerImage.color = gradient.Evaluate(time / timeLimit);
        } else
        {
            this.GetComponent<QuestionMechanic>().submitNotAnswered();
        }
    }

    public float getFillAmount()
    {
        return timerImage.fillAmount;
    }

    public Color getColor()
    {
        return timerImage.color;
    }

    public void setPersonSprite(Sprite sp)
    {
        person.sprite = sp;
    }

    public void randomAnswer()
    {
        this.GetComponent<QuestionMechanic>().submitRandom();
    }

    private void activate(bool activate)
    {
        isActive = activate;

        this.GetComponent<QuestionMechanic>().po.GetComponent<PersonSetUp>().activate(activate);
        this.GetComponent<QuestionMechanic>().nqisv.GetComponent<OpenQuestion>().select(activate);
        this.transform.GetChild(0).gameObject.SetActive(activate);

        if (activate)
        {
            this.GetComponent<Animator>().Rebind();
            this.GetComponent<Animator>().Update(0f);
        }
    }

    public void hide()
    {
        activate(false);
    }

    public void show()
    {
        activate(true);
    }
}
