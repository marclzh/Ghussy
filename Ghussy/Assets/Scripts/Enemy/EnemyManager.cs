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
    }

    // Update is called once per frame
    void Update()
    {
       DevKey_KillAllEnemies();
    }

    private void DevKey_KillAllEnemies()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            int i = 0;
            int total = numOfEnemiesLeft;
            while (i < total)
            {

                EnemyHealth tmp = transform.GetChild(i).GetComponent<EnemyHealth>();
                tmp.TakeDamage(9999999999);
                i++;
            }
            return;

        }
    }

    // To be called by OnEnemyDeath Event
    public void UpdateEnemyCount()
    {
        if (numOfEnemiesLeft > 0)
        {
            numOfEnemiesLeft--;
        }

        if (numOfEnemiesLeft <= 0)
        {
            // Spawns Room Reward / Unlock Doors
            onAllEnemiesDead.Raise();
        }
    }


}