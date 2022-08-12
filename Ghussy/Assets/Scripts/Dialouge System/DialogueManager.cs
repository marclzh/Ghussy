using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * This class controls the dialogues in the game.
 */
public class DialogueManager : MonoBehaviour
{
	// Event to signify the end of the dialogue.
	[SerializeField] VoidEvent onDialogueEnd;

	// References to the Texts and Images used in the dialogue.
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public Image currTalkerImage;

	// Reference to the pause menu of the game.
	[SerializeField] private PauseMenu pauseMenu;

	// Reference to the animator of the dialogue box.
	public Animator animator;

	// Queues controlling the display of the dialogues.
	private Queue<string> sentences;
	private Queue<string> names;
	private Queue<Sprite> sprites;

	void Start()
	{
		// Initializing the queues for the dialogues.
		sentences = new Queue<string>();
		names = new Queue<string>();
		sprites = new Queue<Sprite>();
	}

	// Method to start the dialogue when called.
	public void StartDialogue(Dialogue dialogue)
	{
		if (FindObjectOfType<Player>() != null)
		{
			FindObjectOfType<Player>().GetComponent<PlayerAnimator>().PlayerIdle();
		}

		if (pauseMenu != null)
		{			
			pauseMenu.DisablePausing();
			Debug.Log("menu disabled");
		}
		// Plays dialogue box animation.
		animator.SetBool("IsOpen", true);
		
		// Clearing the queues of old names etc.
		names.Clear(); 
		sentences.Clear();
		sprites.Clear();

		// Enqueuing the dialogues, names and sprites into the respective queues.
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		foreach (string name in dialogue.names)
		{
			names.Enqueue(name);
		}

		foreach(Sprite sprite in dialogue.sprites)
        {
			sprites.Enqueue(sprite);
        }

		DisplayNextSentence();		
	}

	// Method to display the next sentnce of the dialogue.
	public void DisplayNextSentence()
	{
		// Audio
		AudioManager.Instance.Play("Click");

		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		string name = names.Dequeue();
		Sprite sprite = sprites.Dequeue();
		StopAllCoroutines();
		// Display Sentences in dialogue
		StartCoroutine(TypeSentence(sentence));
		// Updates name for each sentence
		nameText.text = name;
		// Updates image for new name
		currTalkerImage.sprite = sprite;
	}

	// Method for the "animation" of the dialogue typed out.
	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(.005f);
		}
	}

	// Method to end the dialogue.
	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		onDialogueEnd.Raise();
		if (pauseMenu != null)
		{
			pauseMenu.EnablePausing();
		}
	}
}