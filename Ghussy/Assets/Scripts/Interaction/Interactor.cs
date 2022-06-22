using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.18f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider2D[] colliders = new Collider2D[3];
    [SerializeField] private int numFound;
    private IInteractable interactable;
    //private bool interacted = false;

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
                if (true)
                {
                    if (!interactionPromptUI.isDisplayed) interactionPromptUI.SetUp(interactable.InteractionPrompt);

                    if (Keyboard.current.eKey.wasPressedThisFrame)
                    {
                       //interacted = true;
                       interactable.Interact(this);
                       if (interactionPromptUI.isDisplayed) interactionPromptUI.Close();
                     } 
                }
                else
                {
                    if (interactionPromptUI.isDisplayed) interactionPromptUI.Close();
                }


             }
        } 
        else
        {
            if (interactable != null) interactable = null;
            if (interactionPromptUI.isDisplayed) interactionPromptUI.Close();
        }
    }
    
    // Show interaction radius
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
    

}
