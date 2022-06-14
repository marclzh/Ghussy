using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEctoplasmCollector : MonoBehaviour
{ 
    private int currentEctoplasmAmount = 0;
    [SerializeField] private int minAmount;
    [SerializeField] private int maxAmount;
    [SerializeField] private IntEvent onEctoplasmAmountUpdate;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ectoplasm"))
        {
            currentEctoplasmAmount += Random.Range(minAmount, maxAmount);
            onEctoplasmAmountUpdate.Raise(currentEctoplasmAmount);
            Destroy(collision.gameObject);
        }
    }

}
