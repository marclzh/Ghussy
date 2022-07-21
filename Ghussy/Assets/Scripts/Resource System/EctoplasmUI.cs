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

    public void Start()
    {
        currentEctoplasmAmount = SaveManager.instance.hasLoaded ? ectoplasmInventory.Container.Count > 0 ? ectoplasmInventory.Container[0].amount : 0 : 0;
        EctoplasmText.text = SaveManager.instance.hasLoaded ? ectoplasmInventory.Container.Count > 0 ? ectoplasmInventory.Container[0].amount.ToString() : "0" : "0";
    }


    public void DisplayCurrentEctoplasmAmount()
    { 
        EctoplasmText.text = ectoplasmInventory.Container.Count > 0 ? ectoplasmInventory.Container[0].amount.ToString() : "0";
    }

    /*
    public void FixedUpdate()
    {
        // Uses ectoplasm inventory which is meant to only hold ectoplasm
        if (ectoplasmInventory.Container.Count > 0)
        {
            EctoplasmText.text = ectoplasmInventory.Container[0].amount.ToString();
        }
  
    }
    */
}
