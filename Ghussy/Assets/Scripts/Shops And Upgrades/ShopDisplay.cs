using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] Transform canvas;
    [SerializeField] Image bossSkeleton;
    [SerializeField] Image minusBossHealth;
    [SerializeField] Image minusEnemy;
    [SerializeField] Button bossSkeletonButton;
    [SerializeField] Button minusBossHealthButton;
    [SerializeField] Button minusEnemyButton;
    [SerializeField] TextMeshProUGUI costText;
    private int itemValue;
    private string selectedItem;

    /* ******************
     * * Item Cost List *
     * ******************
     * BossSkeleton - 15
     * MinusBossHealth - 10
     * MinusEnemy - 5
     */

    // Storing Info of the items
    int bossSkeletonValue;
    int minusBossHealthValue;
    int minusEnemyValue;

    private void Start()
    {
        bossSkeletonValue = 15;
        minusBossHealthValue = 10;
        minusEnemyValue = 5;
    }
    // Swaps the necessary components on buttonclick
    public void ChangeActiveItem(string itemName)
    {
        if (itemName == "BossSkeleton")
        {
            costText.text = bossSkeletonValue.ToString();
            // changes the internal value for CanBuy Method
            itemValue = bossSkeletonValue;
            // changes internal selected item
            selectedItem = itemName;
            bossSkeleton.gameObject.SetActive(true);
            minusBossHealth.gameObject.SetActive(false);
            minusEnemy.gameObject.SetActive(false);
        }

        if (itemName == "MinusBossHealth")
        {
            costText.text = minusBossHealthValue.ToString();
            itemValue = minusBossHealthValue;
            selectedItem = itemName;
            bossSkeleton.gameObject.SetActive(false);
            minusBossHealth.gameObject.SetActive(true);
            minusEnemy.gameObject.SetActive(false);
        }
        
        if (itemName == "MinusEnemy")
        {
            costText.text = minusEnemyValue.ToString();
            itemValue = minusEnemyValue;
            selectedItem = itemName;
            bossSkeleton.gameObject.SetActive(false);
            minusBossHealth.gameObject.SetActive(false);
            minusEnemy.gameObject.SetActive(true);
        }
    }

    public bool CanPurchase()
    {
        Player player = FindObjectOfType<Player>();
        if (player.ectoplasmInventory.Container.Count > 0)
        {
            if (player.ectoplasmInventory.Container[0].amount >= itemValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void Purchase()
    {
        if (CanPurchase())
        {
 
        }
    }

    public void CloseDisplay()
    {
        Debug.Log("Display Closed");
        FindObjectOfType<PlayerController>().ActionMapPlayerChange();
        canvas.gameObject.SetActive(false);
    }
}
