using UnityEngine;

/**
 * This class controls the dialogue triggers in the tutorial scenes.
 */
public class DialogueTrigger : MonoBehaviour
{
	// Reference to the dialogue.
	public Dialogue dialogue;

	// Triggers the next dialogue to be played.
	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
	
	// Disables the entered Trigger.
	public void DisableTrigger()
    {
		gameObject.SetActive(false);
    }

}