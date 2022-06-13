using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionManager : MonoBehaviour
{
    private BasePossessionState currentState;

   public void Transform(BasePossessionState nextState)
    {
        if (nextState != currentState)
        {
            // Insert transformation logic here
        }

        Debug.Log("transformation works!!");
    }
}
