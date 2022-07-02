using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int numOfEnemiesLeft;
    [SerializeField] private VoidEvent onAllEnemiesDead;

    void Start()
    {
        numOfEnemiesLeft = transform.childCount;
        Debug.Log("Enemies Left: " + numOfEnemiesLeft);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // To be called by OnEnemyDeath Event
    public void UpdateEnemyCount()
    {
        if (numOfEnemiesLeft > 0)
        {
            numOfEnemiesLeft--;
            Debug.Log("Enemies Left: " + numOfEnemiesLeft);
        }

        if (numOfEnemiesLeft <= 0)
        {
            // Spawns Room Reward / Unlock Doors
            onAllEnemiesDead.Raise();
        }
    }

    
}