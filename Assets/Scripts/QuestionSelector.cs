using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSelector : MonoBehaviour
{
    public static QuestionSelector instance;

    public QuestionObject[] questions;

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

    public QuestionObject getRandomQuestion()
    {
        return questions[Random.Range(0, questions.Length)];
    }
}
