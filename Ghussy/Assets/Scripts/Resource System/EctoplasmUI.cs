using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EctoplasmUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EctoplasmText;
    [SerializeField] private InventoryObject ectoplasmInventory;
    private int currentEctoplasmAmount = 0;
   
    // can remove this
    public void DisplayCurrentEctoplasmAmount(int amount)
    {
        currentEctoplasmAmount = amount;
    }


    public void FixedUpdate()
    {
        // Uses ectoplasm inventory which is meant to only hold ectoplasm
        if (ectoplasmInventory.Container.Count > 0)
        {
            EctoplasmText.text = ectoplasmInventory.Container[0].amount.ToString();
        }
  
        
    }
}
