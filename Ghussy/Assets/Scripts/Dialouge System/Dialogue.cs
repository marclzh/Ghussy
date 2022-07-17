using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{

	public string[] names;

	public Sprite[] sprites;

	[TextArea(3, 10)]
	public string[] sentences;

}