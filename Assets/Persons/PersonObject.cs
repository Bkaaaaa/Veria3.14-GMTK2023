using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PersonData", order = 2)]
public class PersonObject : ScriptableObject
{
    public enum influence
    {
        inexistant,
        normal,
        popular
    }

    public Sprite sprite;

    public string nameText;
    public string firstnameText;
    public string jobText;
    public influence influenceText;

    public QuestionObject[] questions;
    public bool[] asked;

    public string ID;
}
