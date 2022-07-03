using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] int numOfInteractables;

    private void Start()
    {
        numOfInteractables = transform.childCount;
    }

    public void SetAllInteractablesActive()
    {
        for (int i = 0; i < numOfInteractables; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
