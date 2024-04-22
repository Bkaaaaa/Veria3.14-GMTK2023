using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenQuestion : MonoBehaviour
{
    public Image timerImage;
    public Image backgroundImage;
    public Image personImage;
    public QuestionSetUp question;
    public TextMeshProUGUI personText;

    public Color normalColor;
    public Color selectedColor;


    public void open()
    {
        QuestionManager.instance.hideAll();

        question.show();
    }

    public void delete()
    {
        question.GetComponent<QuestionMechanic>().submitNotAnswered();
    }

    public void quickAnswer()
    {
        question.randomAnswer();
    }

    public void select(bool select)
    {
        backgroundImage.color = select ? selectedColor : normalColor;
    }

    public void setQuestion(GameObject q)
    {
        question = q.GetComponent<QuestionSetUp>();
    }

    public void setPersonSprite(Sprite sp, string name)
    {
        personImage.sprite = sp;
        personText.text = name;
    }

    private void Update()
    {
        // Update time limit
        if (question.getFillAmount() < 1f)
        {
            timerImage.fillAmount = question.getFillAmount();
            timerImage.color = question.getColor();
        }
    }
}
