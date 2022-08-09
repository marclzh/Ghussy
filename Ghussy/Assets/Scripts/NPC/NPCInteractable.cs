using UnityEngine;

/**
 * This class controls the NPC interactable logic.
 */
public class NPCInteractable : MonoBehaviour, IInteractable
{
    // Reference to the dialogue trigger of the NPC's dialogue.
    [SerializeField] private DialogueTrigger dialogueTrigger;
    // Prompt shown upon coming close to the NPC interactable.
    [SerializeField] private string prompt;
    // Event to signify the start of a dialogue.
    [SerializeField] private VoidEvent OnDialogueStart;
    public string InteractionPrompt => prompt;

    // Interact Method to interact with the NPC.
    public bool Interact(Interactor interactor)
    {
        OnDialogueStart.Raise();
        dialogueTrigger.TriggerDialogue();
        return true;
    }

    // Sets the trigger of the dialogue to the correct one.
    public void SetTrigger(DialogueTrigger trigger)
    {
        dialogueTrigger = trigger;
    }
}

