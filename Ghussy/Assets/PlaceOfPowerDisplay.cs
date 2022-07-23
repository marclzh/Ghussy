using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceOfPowerDisplay : MonoBehaviour
{
    [SerializeField] Transform canvas;

    private float movementSpeedLevel;
    private float maxHealthLevel;
    private float projectileSizeLevel;

    [SerializeField] Image MS1;
    [SerializeField] Image MS2;
    [SerializeField] Image MS3;
    [SerializeField] Image MH1;
    [SerializeField] Image MH2;
    [SerializeField] Image MH3;
    [SerializeField] Image PS1;
    [SerializeField] Image PS2;
    [SerializeField] Image PS3;

    [SerializeField] Color purchasedColour;


    void Start()
    {
        movementSpeedLevel = SaveManager.instance.activeSave.permBoonMultiple[0];
        maxHealthLevel = SaveManager.instance.activeSave.permBoonMultiple[1];
        projectileSizeLevel = SaveManager.instance.activeSave.permBoonMultiple[2];
        UpdateUI();
    }

    public void PurchaseMS()
    {
        if (CanPurchase() && movementSpeedLevel < 3) 
        {
            movementSpeedLevel++;
            SaveManager.instance.activeSave.permBoonMultiple[0] = movementSpeedLevel;
            UpdateUI();
            AudioManager.Instance.Play("PurchasePass");

            FindObjectOfType<Player>().ApplyPermBoons();

            SaveManager.instance.activeSave.permBoonApplied = true;
            SaveManager.instance.SaveGame();
        }
        else
        {
            AudioManager.Instance.Play("PurchaseFail");
        }

    }

    public void PurchaseMH()
    {
        if (CanPurchase() && maxHealthLevel < 3)
        {
            maxHealthLevel++;
            SaveManager.instance.activeSave.permBoonMultiple[1] = maxHealthLevel;
            UpdateUI();

            AudioManager.Instance.Play("PurchasePass");

            FindObjectOfType<Player>().ApplyPermBoons();

            SaveManager.instance.activeSave.permBoonApplied = true;
            SaveManager.instance.SaveGame();


        } 
        else
        {
            AudioManager.Instance.Play("PurchaseFail");
        }
    }

    public void PurchasePS()
    {
        if (CanPurchase() && projectileSizeLevel < 3)
        {
            projectileSizeLevel++;
            SaveManager.instance.activeSave.permBoonMultiple[2] = projectileSizeLevel;
            UpdateUI();

            AudioManager.Instance.Play("PurchasePass");

            FindObjectOfType<Player>().ApplyPermBoons();

            SaveManager.instance.activeSave.permBoonApplied = true;
            SaveManager.instance.SaveGame();
        }
        else
        {
            AudioManager.Instance.Play("PurchaseFail");
        }

        
    }


    public bool CanPurchase() 
    {
        
        Player player = FindObjectOfType<Player>();
        if (player.ectoplasmInventory.Container.Count > 0)
        {
            if (player.ectoplasmInventory.Container[0].amount >= 30)
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

    private void UpdateUI()
    {
        if (movementSpeedLevel >= 1) { MS1.color = purchasedColour; }
        if (movementSpeedLevel >= 2) { MS2.color = purchasedColour; MS2.color = purchasedColour; }
        if (movementSpeedLevel >= 3) { MS3.color = purchasedColour; MS2.color = purchasedColour; MS3.color = purchasedColour; }

        if (maxHealthLevel >= 1) { MH1.color = purchasedColour; }
        if (maxHealthLevel >= 2) { MH2.color = purchasedColour; MH2.color = purchasedColour; }
        if (maxHealthLevel >= 3) { MH3.color = purchasedColour; MH2.color = purchasedColour; MH3.color = purchasedColour; }

        if (projectileSizeLevel >= 1) { PS1.color = purchasedColour; }
        if (projectileSizeLevel >= 2) { PS2.color = purchasedColour; PS2.color = purchasedColour; }
        if (projectileSizeLevel >= 3) { PS3.color = purchasedColour; PS2.color = purchasedColour; PS3.color = purchasedColour; }

    }

    private void Update()
    {
        Player player = FindObjectOfType<Player>();
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (player.ectoplasmInventory.Container.Count > 0)
            {
                player.ectoplasmInventory.Container[0].amount += 1000;
            }
            else
            {
                
            }
        }
    }

    public void CloseDisplay()
    {
        FindObjectOfType<PlayerController>().ActionMapPlayerChange();
        canvas.gameObject.SetActive(false);
    }
}
