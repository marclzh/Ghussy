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
    [SerializeField] VoidEvent OnMemoryShardDeducted;
    [SerializeField] Image Purchased1;
    [SerializeField] Image Purchased2;
    [SerializeField] Image Purchased3;
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
        bossSkeletonValue = 150;
        minusBossHealthValue = 200;
        minusEnemyValue = 250;

        UpdateShopUI();
    }
    // Swaps the necessary components on buttonClick
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
        if (player.memoryShardInventory.Container.Count > 0)
        {
            if (player.memoryShardInventory.Container[0].amount >= itemValue)
            {
                if (selectedItem == "BossSkeleton") { return !(SaveManager.instance.activeSave.shopBossSkeletonPurchased); }
                if (selectedItem == "MinusBossHealth") { return !(SaveManager.instance.activeSave.shopBossHealthDeductionPurchased); }
                if (selectedItem == "MinusEnemy") { return !(SaveManager.instance.activeSave.shopEnemyNumberDeductionPurchased); }
            }
        }

        // Audio Cue
        AudioManager.Instance.Play("PurchaseFail");

        return false;
    }

    public void Purchase()
    {
        if (CanPurchase())
        {
            // Audio Cue
            AudioManager.Instance.Play("PurchasePass");

            // Deducts MemoryShard from Player
            DeductMemoryShard();

            // Update Save Manager
            if (selectedItem == "BossSkeleton") { SaveManager.instance.activeSave.shopBossSkeletonPurchased = true; }
            if (selectedItem == "MinusBossHealth") { SaveManager.instance.activeSave.shopBossHealthDeductionPurchased = true; }
            if (selectedItem == "MinusEnemy") { SaveManager.instance.activeSave.shopEnemyNumberDeductionPurchased = true; }
            SaveManager.instance.SaveGame();

            // Update UI
            UpdateShopUI();
        }
    }

    public void DeductMemoryShard()
    {
        FindObjectOfType<Player>().purchaseBoon(itemValue, ResourceType.MemoryShard);
        OnMemoryShardDeducted.Raise();
    }

    public void UpdateShopUI()
    {
        if (SaveManager.instance.activeSave.shopBossSkeletonPurchased == true) { Purchased1.gameObject.SetActive(true); }
        if (SaveManager.instance.activeSave.shopBossHealthDeductionPurchased == true) { Purchased2.gameObject.SetActive(true); }
        if (SaveManager.instance.activeSave.shopEnemyNumberDeductionPurchased == true) { Purchased3.gameObject.SetActive(true); }
    }

    public void CloseDisplay()
    {
        FindObjectOfType<PlayerController>().ActionMapPlayerChange();
        canvas.gameObject.SetActive(false);
    }
}
