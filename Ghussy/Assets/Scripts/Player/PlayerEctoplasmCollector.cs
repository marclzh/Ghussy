using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEctoplasmCollector : MonoBehaviour
{ 
    private int currentEctoplasmAmount = 0;
    [SerializeField] private int minAmount;
    [SerializeField] private int maxAmount;
    [SerializeField] private IntEvent onEctoplasmAmountUpdate;
    [SerializeField] private InventoryObject ectoplasmInventory;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ectoplasm"))
        {
            var item = collision.GetComponent<Item>();

            if (item)
            {
                int ectoplasmGained = Random.Range(minAmount, maxAmount);
                currentEctoplasmAmount += ectoplasmGained;
                ectoplasmInventory.AddItem(item.item, ectoplasmGained);
                onEctoplasmAmountUpdate.Raise(currentEctoplasmAmount);
                Destroy(collision.gameObject);
            }

            
        }
    }

}
