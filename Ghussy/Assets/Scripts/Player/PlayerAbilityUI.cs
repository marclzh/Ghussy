using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityUI : MonoBehaviour
{
    [SerializeField] private BasePossessionState defaultState;
    private Image abilityImage;
    private Sprite nextImage;
    private float coolDownTime; 
    private bool OnCoolDown = false;
    [SerializeField] private KeyCode abilityKey;

    
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
        if (Input.GetKeyDown(abilityKey) && OnCoolDown == false)
        {
            Debug.Log("key pressed");
            OnCoolDown = true;
            abilityImage.fillAmount = 0;
         
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
