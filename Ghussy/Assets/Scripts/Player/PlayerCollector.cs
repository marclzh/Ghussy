using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCollector : MonoBehaviour
{ 
    [SerializeField] private VoidEvent onEctoplasmAmountUpdate;
    [SerializeField] private VoidEvent onMemoryShardAmountUpdate;
    [SerializeField] private InventoryObject ectoplasmInventory;
    [SerializeField] private InventoryObject memoryShardInventory;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ectoplasm"))
        {
            var item = collision.GetComponent<Item>();

            if (item)
            {
                int ectoplasmGained = item.GetComponent<Ectoplasm>().amount;
                ectoplasmInventory.AddItem(item.item, ectoplasmGained);
                onEctoplasmAmountUpdate.Raise();
                Destroy(collision.gameObject);

                // Save Management
                GameObject.FindObjectOfType<SaveManager>().activeSave.ectoplasmAmount = ectoplasmInventory.Container[0].amount;
            }
            
        }

        if (collision.gameObject.CompareTag("MemoryShard"))
        {
            var item = collision.GetComponent<Item>();

            if (item)
            {
                int msGained = item.GetComponent<MemoryShard>().amount;
                memoryShardInventory.AddItem(item.item, msGained);
                onMemoryShardAmountUpdate.Raise();
                Destroy(collision.gameObject);

                // Save Management
                GameObject.FindObjectOfType<SaveManager>().activeSave.memoryShardAmount = memoryShardInventory.Container[0].amount;

            }

        }
    }

}
