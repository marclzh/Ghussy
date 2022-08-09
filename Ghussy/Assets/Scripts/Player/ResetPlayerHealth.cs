using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerHealth : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<PlayerHealth>().Heal(9999);
    }
}
