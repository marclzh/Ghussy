using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopDisplay : MonoBehaviour
{
    // UI References
    [SerializeField] private Transform canvas;
    [SerializeField] private Image bossSkeleton;
    [SerializeField] private Image minusBossHealth;
    [SerializeField] private Image minusEnemy;
    [SerializeField] private Button bossSkeletonButton;
    [SerializeField] private Button minusBossHealthButton;
    [SerializeField] private Button minusEnemyButton;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image Purchased1;
    [SerializeField] private Image Purchased2;
    [SerializeField] private Image Purchased3;

    // Helper fields
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private VoidEvent OnMemoryShardDeducted;

    // Current item values
    private int itemValue;
    private string selectedItem;

    // Storing Info of the items
    private int bossSkeletonValue = 150;
    private int minusBossHealthValue = 200;
    private int minusEnemyValue = 250;

    private void Start()
    {
        // Prevent player from pausing when shop is active
        pauseMenu.DisablePausing();

        // Update UI
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

            // changes the internal value for CanBuy Method
            itemValue = minusBossHealthValue;

            // changes internal selected item
            selectedItem = itemName;
            bossSkeleton.gameObject.SetActive(false);
            minusBossHealth.gameObject.SetActive(true);
            minusEnemy.gameObject.SetActive(false);
        }
        
        if (itemName == "MinusEnemy")
        {
            costText.text = minusEnemyValue.ToString();

            // changes the internal value for CanBuy Method
            itemValue = minusEnemyValue;

            // changes internal selected item
            selectedItem = itemName;
            bossSkeleton.gameObject.SetActive(false);
            minusBossHealth.gameObject.SetActive(false);
            minusEnemy.gameObject.SetActive(true);
        }
    }

    // Checks if the player has enough currency to purchase this item and has not purchased this item already
    public bool CanPurchase()
    {
        // Retrieve player reference
        Player player = FindObjectOfType<Player>();

        if (player.memoryShardInventory.Container.Count > 0)
        {
            // Check player currency amount
            if (player.memoryShardInventory.Container[0].amount >= itemValue)
            {
                // Check if player has already purchased this item
                if (selectedItem == "BossSkeleton") { return !(SaveManager.instance.activeSave.shopBossSkeletonPurchased); }
                if (selectedItem == "MinusBossHealth") { return !(SaveManager.instance.activeSave.shopBossHealthDeductionPurchased); }
                if (selectedItem == "MinusEnemy") { return !(SaveManager.instance.activeSave.shopEnemyNumberDeductionPurchased); }
            }
        }

        // Audio Cue
        AudioManager.Instance.Play("PurchaseFail");

        return false;
    }

    // Purchase currently selected item
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

    // Deducts corresponding currency when a purchase is made
    public void DeductMemoryShard()
    {
        FindObjectOfType<Player>().purchaseBoon(itemValue, ResourceType.MemoryShard);

        // Raise event to update player HUD UI
        OnMemoryShardDeducted.Raise();
    }

    // Update UI
    public void UpdateShopUI()
    {
        if (SaveManager.instance.activeSave.shopBossSkeletonPurchased == true) { Purchased1.gameObject.SetActive(true); }
        if (SaveManager.instance.activeSave.shopBossHealthDeductionPurchased == true) { Purchased2.gameObject.SetActive(true); }
        if (SaveManager.instance.activeSave.shopEnemyNumberDeductionPurchased == true) { Purchased3.gameObject.SetActive(true); }
    }

    // Closes shop display
    public void CloseDisplay()
    {
        // Allow player to move
        FindObjectOfType<PlayerController>().ActionMapPlayerChange();
        
        // Closes display
        canvas.gameObject.SetActive(false);
        
        // Allow player to pause
        pauseMenu.EnablePausing();
    }
}
