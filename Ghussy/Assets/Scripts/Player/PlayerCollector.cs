using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles the collection logic of resources which drop from enemies when killed
 */
public class PlayerCollector : MonoBehaviour
{ 
    // Events to signal update to resources when collected
    [SerializeField] private VoidEvent onEctoplasmAmountUpdate;
    [SerializeField] private VoidEvent onMemoryShardAmountUpdate;
    
    // Player Resource Inventories
    [SerializeField] private InventoryObject ectoplasmInventory;
    [SerializeField] private InventoryObject memoryShardInventory;

    // Called when item with resource tagging collides with the player hitbox
    private void OnTriggerEnter2D (Collider2D collision)
    {
        // Item Collected is of type Ectoplasm
        if (collision.gameObject.CompareTag("Ectoplasm"))
        {
            var item = collision.GetComponent<Item>();

            if (item)
            {
                // Retrieve the amount of ectoplasm
                int ectoplasmGained = item.GetComponent<Ectoplasm>().amount;

                // Add ectoplasm into player's inventory 
                ectoplasmInventory.AddItem(item.item, ectoplasmGained);

                // Raise event to update UI
                onEctoplasmAmountUpdate.Raise();

                // Destroy the incoming resource object
                Destroy(collision.gameObject);

                // Save Management
                GameObject.FindObjectOfType<SaveManager>().activeSave.ectoplasmAmount = ectoplasmInventory.Container[0].amount;
            }
            
        }

        // Item collected is of type Memory Shard
        if (collision.gameObject.CompareTag("MemoryShard"))
        {
            var item = collision.GetComponent<Item>();

            if (item)
            {
                // Retrieve the amount of Memory Shards
                int msGained = item.GetComponent<MemoryShard>().amount;

                // Add memory shard into player's inventory
                memoryShardInventory.AddItem(item.item, msGained);

                // Raise event to update UI
                onMemoryShardAmountUpdate.Raise();

                // Destory the incoming resource object
                Destroy(collision.gameObject);

                // Save Management
                GameObject.FindObjectOfType<SaveManager>().activeSave.memoryShardAmount = memoryShardInventory.Container[0].amount;

            }

        }
    }

}
