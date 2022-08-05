using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnemy : MonoBehaviour
{
    [SerializeField] private float delay;

    // Start is called before the first frame update
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
