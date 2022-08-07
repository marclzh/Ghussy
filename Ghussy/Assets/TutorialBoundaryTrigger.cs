using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoundaryTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        return;
    }
}
