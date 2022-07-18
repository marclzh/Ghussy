using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] int numOfDoors;

    private void Start()
    {
        numOfDoors = transform.childCount;
    }

    public void SetAllInteractablesActive()
    {
        for (int i = 0; i < numOfDoors; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
