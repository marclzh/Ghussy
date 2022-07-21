using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryShardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI memoryShardText;
    [SerializeField] private InventoryObject memoryShardInventory;
    private int currentMemoryShardAmount = 0;

    public void Start()
    {
        currentMemoryShardAmount = SaveManager.instance.hasLoaded ? memoryShardInventory.Container.Count > 0 ? memoryShardInventory.Container[0].amount : 0 : 0;
        memoryShardText.text = SaveManager.instance.hasLoaded ? memoryShardInventory.Container.Count > 0 ? memoryShardInventory.Container[0].amount.ToString() : "0" : "0";
    }
    public void DisplayCurrentAmount()
    {
        memoryShardText.text = memoryShardInventory.Container.Count > 0 ? memoryShardInventory.Container[0].amount.ToString() : "0";
    }
    /*
    public void FixedUpdate()
    {
        // Uses ectoplasm inventory which is meant to only hold ectoplasm
        if (memoryShardInventory.Container.Count > 0)
        {
            memoryShardText.text = memoryShardInventory.Container[0].amount.ToString();
        }

    }
    */


}
