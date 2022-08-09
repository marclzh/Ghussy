using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Summons Enemies after a set delay, used in boss room
public class SummonEnemy : MonoBehaviour
{
    // Delay Time
    [SerializeField] private float delay;

    public void StartDelay()
    {
        StartCoroutine(DelaySpawn());
    }

    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(delay);

        // Audio Queue
        AudioManager.Instance.Play("MinionSummon");

        // Set Enemies Active
        transform.GetChild(0).gameObject.SetActive(true);
    }
    
}
