using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/**
 * This script controls the logic of the player's ability.
 */
public class PlayerAbility : MonoBehaviour
{
    // Reference to the default state of the player.
    [SerializeField] private BasePossessionState defaultState;
    // Reference to the Image of the ability.
    private Image abilityImage;
    // Reference to the Sprite of the next ability.
    private Sprite nextImage;
    // Length of the ability.
    private float abilityTimer;
    // Cooldown time of the ability.
    private float coolDownTime;
    // Boolean to check that the ability is active.
    private bool abilityActive = false;
    // Boolean to check if the ability is on cooldown.
    private bool OnCoolDown = false;
    // Reference to the current weapon the player is using.
    [SerializeField] private PlayerWeapon playerWeapon;
    // Boolean to check if the ability key is pressed.
    private bool abilityKeyPressed;

    void Start()
    {
        abilityImage = GameObject.FindGameObjectWithTag("AbilityImage").GetComponent<Image>();
        GetAbilityInfo(defaultState);
        abilityImage.fillAmount = 1;
    }

    // Method to get the current abillity information from the ability ScriptableObject.
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

    // Method to update the UI of the ability.
    private void UpdateUI()
    {
        abilityImage.sprite = nextImage;
    }

    private void Update()
    {
        UseAbility();
    }

    // Specific Namespace for New Input System - Do not change metrhod name
    private void OnAbility(InputValue value)
    {
        abilityKeyPressed = value.isPressed;
    }

    // Method to control the usage of the ability.
    void UseAbility()
    {
        if (abilityKeyPressed && OnCoolDown == false && abilityActive == false &&
            GetComponent<Player>().currentState != defaultState)
        {
            AudioManager.Instance.Play("Ability");
            abilityActive = true;
            playerWeapon.AbilityActivate();
            abilityKeyPressed = false;
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

