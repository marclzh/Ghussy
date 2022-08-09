using UnityEngine;

/**
 * This class is used for the dialogue manager class to 
 * centralise the information for the dialogues.
 */

[System.Serializable]
public class Dialogue
{
	// Name of the speaker.
	public string[] names;

	// Sprite of the speaker.
	public Sprite[] sprites;

	// Sentence being spoken.
	[TextArea(3, 10)]
	public string[] sentences;

}