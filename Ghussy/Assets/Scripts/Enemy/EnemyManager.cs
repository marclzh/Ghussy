using System.Collections;
using UnityEngine;

/**
 * Class to control the enemy manager.
 */
public class EnemyManager : MonoBehaviour
{
    // Reference to the number of enemies left.
    [SerializeField] private int numOfEnemiesLeft;
    // Event to signify all the enemy's death in the scene.
    [SerializeField] private VoidEvent onAllEnemiesDead;
    // Boolean to check if the shop item has been purchased.
    [SerializeField] private bool enemyNumberDeductionPurchased;

    void Start()
    {
        // Check if item has been purchased
        enemyNumberDeductionPurchased = SaveManager.instance.activeSave.shopEnemyNumberDeductionPurchased;
        if (enemyNumberDeductionPurchased) { Destroy(transform.GetChild(0).gameObject); } // Destroys first child 
        StartCoroutine(DelayChildCount());
    }

    // Destroyed Game Objects are only updated at the end of the given frame
    IEnumerator DelayChildCount()
    {
        yield return new WaitForEndOfFrame();
        numOfEnemiesLeft = transform.childCount;
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