using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This class controls the Interactions of the player with interactables.
 */
public class Interactor : MonoBehaviour
{
    // Reference to the interaction radius that the player can interact with.
    [SerializeField] private Transform interactionPoint;
    // Size of the radius that interactables can be interacted with.
    [SerializeField] private float interactionPointRadius = 0.18f;
    // Layer that the interactables can be detected on.
    [SerializeField] private LayerMask interactableMask;
    // Reference to the interaction prompt that pops up.
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    // Array of colliders detected within the interaction radius.
    private readonly Collider2D[] colliders = new Collider2D[3];
    // Number of colliders found.
    [SerializeField] private int numFound;
    // Reference to the interactable interface.
    private IInteractable interactable;
    // Initializing interacted boolean.
    private bool interacted = false;

    private void Update()
    {
        ContactFilter2D cf = new ContactFilter2D();
        cf.SetLayerMask(interactableMask);
        numFound = Physics2D.OverlapCircle(interactionPoint.position, interactionPointRadius, cf, colliders);

        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                
                    if (!interactionPromptUI.isDisplayed) interactionPromptUI.SetUp(interactable.InteractionPrompt);

                    if (interacted)
                    {                
                       interactable.Interact(this);
                       if (interactionPromptUI.isDisplayed) interactionPromptUI.Close();
                       interacted = false;
                     } 
                

             }
        } 
        else
        {
            if (interactable != null) interactable = null;
            if (interactionPromptUI.isDisplayed) interactionPromptUI.Close();
        }
    }

    // Specific Namespace for New Input System - Do not change method name
    private void OnInteract(InputValue value)
    {
        interacted = value.isPressed;
    }

}
