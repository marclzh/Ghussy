using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	[SerializeField] VoidEvent onDialogueEnd;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public Image currTalkerImage;

	public Animator animator;

	private Queue<string> sentences;
	private Queue<string> names;
	private Queue<Sprite> sprites;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
		names = new Queue<string>();
		sprites = new Queue<Sprite>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);
		
		// Clearing the queues of old names etc.
		names.Clear(); 
		sentences.Clear();
		sprites.Clear();

		// Enqueuing the dialogues into the queues
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

	public void DisplayNextSentence()
	{
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

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(.005f);
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		onDialogueEnd.Raise();		
	}
}