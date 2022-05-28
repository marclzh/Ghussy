using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private PlayerAnimator playerAnimator;
    public Image abilityImage;
    public float coolDownTime;
    bool OnCoolDown = false;
    public KeyCode Ability1;

    // Start is called before the first frame update
    void Start()
    {
        abilityImage.fillAmount = 0;
    }

    private void Update()
    {
        UseAbility(); 
    }

    void UseAbility()
    {
        if (Input.GetKey(Ability1) && OnCoolDown == false)
        {
            OnCoolDown = true;
            abilityImage.fillAmount = 1;
            // Animation
            playerAnimator.PlayerTransform();
        }

        if (OnCoolDown)
        {
            abilityImage.fillAmount -= 1 / coolDownTime * Time.deltaTime;

            if(abilityImage.fillAmount <= 0)
            {
                abilityImage.fillAmount = 0;
                OnCoolDown = false;
            }
        }
    }
}
