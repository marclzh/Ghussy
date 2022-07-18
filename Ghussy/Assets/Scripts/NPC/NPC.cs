using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, INPC
{
    private NPCInteractable npcInteractable;
    [SerializeField] private Transform dialogues;
    private GameObject[] dialoguesArr;
    private int dialogueCount;
    private int dialogueCounter;
    private int initDialogueCounter;

    public void Awake()
    {
        // Initialising Variables
        npcInteractable = GetComponent<NPCInteractable>();
        dialogueCount = dialogues.childCount;

        // Arr of possible Dialogues
        dialoguesArr = new GameObject[dialogueCount];

        for (int i = 0; i < dialoguesArr.Length; i++)
        {
            dialoguesArr[i] = dialogues.GetChild(i).gameObject;
        }
    }

    public void SwitchTrigger()
    {
        Debug.Log("Dialogue Trigger switched");
        npcInteractable.SetTrigger(dialoguesArr[dialogueCounter].GetComponent<DialogueTrigger>());
        dialogueCounter++;
        if (dialogueCounter >= dialogueCount)
        {
            dialogueCounter = initDialogueCounter;
        }
    }

    public void SetDialogueCounter(int count)
    {
        initDialogueCounter = count;
        dialogueCounter = count;
    }
}
