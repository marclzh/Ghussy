using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : Health
{
    [SerializeField] private VoidEvent onTransformationDeath;
    [SerializeField] private FloatEvent onHealthChange;
    [SerializeField] private VoidEvent onPlayerDeath;

    // Reference to the Player Object
    [SerializeField] private GameObject player;
   
    // Actual Shield Value
    private float transformationHealthValue;
 
    // Boolean to keep track of transformation state
    private bool isTransformed;
   


    private void Awake()
    {
        // Initialising the values of the variables
        maxHealth = GetComponent<Player>().maxHealth.Value;
        transformationHealthValue = GetComponent<Player>().maxTransformationHealth.Value;
        currentHealth = maxHealth;
       
        onHealthChange.Raise(currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(99);
        }
    }

    public override void TakeDamage(float damage)
    {
        if (!isTransformed)
        {
            base.TakeDamage(damage);
            onHealthChange.Raise(currentHealth);
        } 
        else
        {
            transformationHealthValue -= damage;
            onHealthChange.Raise(transformationHealthValue);

            if (transformationHealthValue <= 0)
            {
                Debug.Log("transformation dead");
                isTransformed = false;
                onTransformationDeath.Raise();
            }
            
        }

    }


    public override void Die()
    {
        onPlayerDeath.Raise();
    }


    public void TransformationUpdateHealth(BasePossessionState nextState) 
    {
        if (nextState != null)
        {
            isTransformed = true;
        }
    }

}
