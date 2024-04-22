using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestionData", order = 1)]
public class QuestionObject : ScriptableObject
{
    public string question = "A normal question";

    public string answerGood = "Good answer";
    public string answerHalf = "Half answer";
    public string answerNeutral = "Neutral answer";
    public string answerWTF = "WTF answer";

    public criteria.list[] criterionImpacted;

    public int[] amountImpacted;

    public string newsGood;
    public string newsHalf;
    public string newsNeutral;
    public string newsWTF;
}
