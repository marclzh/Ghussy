using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private string prompt;
    [SerializeField] private VoidEvent OnDialogueStart;
    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        OnDialogueStart.Raise();
        dialogueTrigger.TriggerDialogue();
        return true;
    }
    public void SetTrigger(DialogueTrigger trigger)
    {
        dialogueTrigger = trigger;
    }
}

