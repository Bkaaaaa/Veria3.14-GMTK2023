using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public criteria.list[] criterionImpacted;
    public int[] amountImpacted;

    void Start()
    {
        if (criterionImpacted.Length == amountImpacted.Length)
        {
            for (int i = 0; i < criterionImpacted.Length; i++)
            {
                Debug.Log(criterionImpacted[i] + " : " + amountImpacted[i]);
            }
        } else
        {
            Debug.LogError("Not as much criteria as amount :thinking:");
        }
    }
}
