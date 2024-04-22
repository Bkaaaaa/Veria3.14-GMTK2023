using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PersonCreator : MonoBehaviour
{
    public static PersonCreator instance;

    public int nbtoCreate;
    public int nbQuestionsPerPerson = 3;

    public string[] names;
    public string[] firstnames;
    public string[] jobs;
    public PersonObject.influence[] influences;

    public Sprite[] sprites;

    public PersonObject[] generalPeople;

    public PersonObject[] people;

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
        people = new PersonObject[nbtoCreate + generalPeople.Length];
        for (int i = 0; i < nbtoCreate; i++)
        {
            people[i] = ScriptableObject.CreateInstance<PersonObject>();

            people[i].ID = i.ToString();

            people[i].nameText = names[Random.Range(0, names.Length)];
            people[i].firstnameText = firstnames[Random.Range(0, firstnames.Length)];
            people[i].jobText = jobs[Random.Range(0, jobs.Length)];
            people[i].influenceText = influences[Random.Range(0, influences.Length)];
            people[i].sprite = sprites[Random.Range(0, sprites.Length)];

            people[i].questions = InstantiateQuestions();
            people[i].asked = new bool[people[i].questions.Length];
        }
        for (int i = nbtoCreate; i < people.Length; i++)
        {
            people[i] = generalPeople[i - nbtoCreate];
        }
    }

    public PersonObject getRandomPerson()
    {
        return people[Random.Range(0, people.Length)];
    }

    private QuestionObject[] InstantiateQuestions()
    {
        QuestionObject[] result = new QuestionObject[nbQuestionsPerPerson];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = QuestionSelector.instance.getRandomQuestion();
        }

        return result;
    }

    public void removePerson(string id)
    {
        int index = -1;

        for (int i = 0; i < people.Length; i++)
        {
            if (people[i].ID.Equals(id))
            {
                index = i;
            }
        }

        PersonObject[] temp = new PersonObject[people.Length - 1];

        for (int i = 0; i < temp.Length; i++)
        {
            if (i < index)
                temp[i] = people[i];
            else if (i > index)
                temp[i] = people[i + 1];
        }

        people = temp;
    }
}
