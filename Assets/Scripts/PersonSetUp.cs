using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonSetUp : MonoBehaviour
{
    public Image icon;

    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI firstnameTMP;
    public TextMeshProUGUI jobTMP;
    public TextMeshProUGUI influenceTMP;

    public Color popInexistantColor;
    public Color popNormalColor;
    public Color popPopularColor;

    public PersonObject po;

    public void setUpUi(PersonObject _po)
    {
        icon.sprite = _po.sprite;

        nameTMP.text = "Name : " + _po.nameText;
        firstnameTMP.text = "Firstname : " + _po.firstnameText;
        jobTMP.text = "Job : " + _po.jobText;

        Color c = Color.white;
        switch (_po.influenceText)
        {
            case global::PersonObject.influence.inexistant:
                c = popInexistantColor;
                break;
            case global::PersonObject.influence.normal:
                c = popNormalColor;
                break;
            case global::PersonObject.influence.popular:
                c = popPopularColor;
                break;
        }
        string hexR = ((int)(c.r * 255)).ToString("X");
        string hexG = ((int)(c.g * 255)).ToString("X");
        string hexB = ((int)(c.b * 255)).ToString("X");

        influenceTMP.text = "Popularity : <#" + hexR + hexG + hexB + ">" + _po.influenceText + "</color>";

        po = _po;
    }

    public void activate(bool activate)
    {
        this.transform.GetChild(0).gameObject.SetActive(activate);
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
