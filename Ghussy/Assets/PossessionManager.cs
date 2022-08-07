using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PossessionManager : MonoBehaviour
{
    
    void Start()
    {
        System.Random rnd = new System.Random();
        int key = rnd.Next(2);

        transform.GetChild(key).gameObject.SetActive(true); 
    }

    
}
