using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityUI : MonoBehaviour
{
    [SerializeField] private BasePossessionState defaultState;
    private Image abilityImage;
    private Sprite nextImage;
    // Activate ability -> can use ability for "abilityTimer" seconds
    // -> Once abilityTimer is up, then coolDownTime activates, then 
    // only once coolDown is false then can reuse ability.
    private float abilityTimer;
    private float coolDownTime;
    private bool abilityActive = false;
    private bool OnCoolDown = false;
    [SerializeField] private KeyCode abilityKey;
    [SerializeField] private PlayerWeapon playerWeapon;


    void Start()
    {
        abilityImage = GameObject.FindGameObjectWithTag("AbilityImage").GetComponent<Image>();
        GetAbilityInfo(defaultState);
        abilityImage.fillAmount = 1;
    }

    public void GetAbilityInfo(BasePossessionState currentState)
    {
        if (currentState != null)
        {
            Ability currentAbility = currentState.GetAbility();
            nextImage = currentAbility.GetImage();
            coolDownTime = currentAbility.GetCooldownTime();
            abilityTimer = currentAbility.GetAbilityTime();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        abilityImage.sprite = nextImage;
    }

    private void Update()
    {
        UseAbility();
    }


    void UseAbility()
    {
        if (Input.GetKeyDown(abilityKey) && OnCoolDown == false && abilityActive == false)
        {      
            abilityActive = true;
            playerWeapon.AbilityActivate();
        }

        if (abilityActive)
        {
            // ability being active drains the gauge, which once finished, 
            // activates the cooldown timer.
            abilityImage.fillAmount -= 1 / abilityTimer * Time.deltaTime;

            if (abilityImage.fillAmount <= 0)
            {
                abilityImage.fillAmount = 0;
                abilityActive = false;
                OnCoolDown = true;
                playerWeapon.AbilityDeactivate();
            }
        }

        if (OnCoolDown)
        {
            abilityImage.fillAmount += 1 / coolDownTime * Time.deltaTime;

            if (abilityImage.fillAmount >= 1)
            {
                abilityImage.fillAmount = 1;
                OnCoolDown = false;
            }
        }
    }
}

