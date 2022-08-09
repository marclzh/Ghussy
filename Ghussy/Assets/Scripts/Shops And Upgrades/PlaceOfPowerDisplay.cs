using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceOfPowerDisplay : MonoBehaviour
{
    // UI Elements
    [SerializeField] private Transform canvas;
    [SerializeField] private Image MS1;
    [SerializeField] private Image MS2;
    [SerializeField] private Image MS3;
    [SerializeField] private Image MH1;
    [SerializeField] private Image MH2;
    [SerializeField] private Image MH3;
    [SerializeField] private Image PS1;
    [SerializeField] private Image PS2;
    [SerializeField] private Image PS3;
    [SerializeField] private Color purchasedColour;

    // Helper fields
    [SerializeField] private VoidEvent OnEctoplasmDeducted;
    [SerializeField] private PauseMenu pauseMenu;

    // Current upgrade level for each upgrade type
    private float movementSpeedLevel;
    private float maxHealthLevel;
    private float projectileSizeLevel;

    // Cost of each upgrade
    private int boonCost = 30;

    void Start()
    {
        // Retrieve upgrade tier levels from save file
        movementSpeedLevel = SaveManager.instance.activeSave.permBoonMultiple[0];
        maxHealthLevel = SaveManager.instance.activeSave.permBoonMultiple[1];
        projectileSizeLevel = SaveManager.instance.activeSave.permBoonMultiple[2];

        // Update UI
        UpdateUI();

        // Prevent player from pausing
        pauseMenu.DisablePausing();
    }

    // Purchase Movement Speed Upgrade
    public void PurchaseMS()
    {
        if (CanPurchase() && movementSpeedLevel < 3) 
        {
            // Increment Upgrade Tier 
            movementSpeedLevel++;
            SaveManager.instance.activeSave.permBoonMultiple[0] = movementSpeedLevel;
            
            // Update Display UI
            UpdateUI();

            // Deduct currency
            DeductEctoplasm();

            // Audio
            AudioManager.Instance.Play("PurchasePass");

            // Apply Upgrade on player
            FindObjectOfType<Player>().ApplyPermBoons();
            SaveManager.instance.activeSave.permBoonApplied = true;
            SaveManager.instance.SaveGame();
        }
        else
        {
            AudioManager.Instance.Play("PurchaseFail");
        }

    }

    // Purchase Maximum Healh Upgrade
    public void PurchaseMH()
    {
        if (CanPurchase() && maxHealthLevel < 3)
        {
            // Increment Upgrade Tier 
            maxHealthLevel++;
            SaveManager.instance.activeSave.permBoonMultiple[1] = maxHealthLevel;

            // Update Display UI
            UpdateUI();

            // Deduct currency
            DeductEctoplasm();

            // Audio
            AudioManager.Instance.Play("PurchasePass");

            // Apply Upgrade on player
            FindObjectOfType<Player>().ApplyPermBoons();
            SaveManager.instance.activeSave.permBoonApplied = true;
            SaveManager.instance.SaveGame();
        } 
        else
        {
            AudioManager.Instance.Play("PurchaseFail");
        }
    }

    // Purchase Projectile Size Upgrade
    public void PurchasePS()
    {
        if (CanPurchase() && projectileSizeLevel < 3)
        {
            // Increment Upgrade Tier 
            projectileSizeLevel++;
            SaveManager.instance.activeSave.permBoonMultiple[2] = projectileSizeLevel;

            // Update Display UI
            UpdateUI();

            // Deduct Currency
            DeductEctoplasm();

            // Audio
            AudioManager.Instance.Play("PurchasePass");
           
            // Apply Upgrades on player
            FindObjectOfType<Player>().ApplyPermBoons();
            SaveManager.instance.activeSave.permBoonApplied = true;
            SaveManager.instance.SaveGame();
        }
        else
        {
            AudioManager.Instance.Play("PurchaseFail");
        }

        
    }

    // Checks if player has sufficient currency
    public bool CanPurchase() 
    {
        // Player reference
        Player player = FindObjectOfType<Player>();

        if (player.ectoplasmInventory.Container.Count > 0)
        {
            if (player.ectoplasmInventory.Container[0].amount >= boonCost)
            {
                
                return true;
            } 
        }

        return false;

    }

    // Updates shop display
    private void UpdateUI()
    {
        // Movement Speed 
        if (movementSpeedLevel >= 1) { MS1.color = purchasedColour; }
        if (movementSpeedLevel >= 2) { MS2.color = purchasedColour; MS1.color = purchasedColour; }
        if (movementSpeedLevel >= 3) { MS3.color = purchasedColour; MS2.color = purchasedColour; MS1.color = purchasedColour; }

        // Max Health
        if (maxHealthLevel >= 1) { MH1.color = purchasedColour; }
        if (maxHealthLevel >= 2) { MH2.color = purchasedColour; MH1.color = purchasedColour; }
        if (maxHealthLevel >= 3) { MH3.color = purchasedColour; MH2.color = purchasedColour; MH1.color = purchasedColour; }

        // Projectile Size
        if (projectileSizeLevel >= 1) { PS1.color = purchasedColour; }
        if (projectileSizeLevel >= 2) { PS2.color = purchasedColour; PS1.color = purchasedColour; }
        if (projectileSizeLevel >= 3) { PS3.color = purchasedColour; PS2.color = purchasedColour; PS1.color = purchasedColour; }

    }

    // Deduct currency
    public void DeductEctoplasm()
    {
        FindObjectOfType<Player>().purchaseBoon(boonCost, ResourceType.Ectoplasm);
     
        // Raise Event to update player HUD UI
        OnEctoplasmDeducted.Raise();
    }
      
    // Closes Place of Power display
    public void CloseDisplay()
    {
        // Enable player movement
        FindObjectOfType<PlayerController>().ActionMapPlayerChange();
        
        // Closes display
        canvas.gameObject.SetActive(false);
        
        // Allow player to pause
        pauseMenu.EnablePausing();
    }
}
