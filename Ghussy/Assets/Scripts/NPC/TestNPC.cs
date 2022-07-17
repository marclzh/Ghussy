using UnityEngine;
using UnityEngine.UI;


public class TestNPC : MonoBehaviour, INPC
{
    private NPCInteractable npcInteractable;
    [SerializeField] private Transform dialogues;
    private GameObject[] dialoguesArr;
    private int dialogueCount;
    private int dialogueCounter;

    public void Start()
    {
        // Initialising Variables
        npcInteractable = GetComponent<NPCInteractable>();
        dialogueCount = dialogues.childCount;
        dialogueCounter = 1;
        
        // Arr of possible Dialogues
        dialoguesArr = new GameObject[dialogueCount];
        
        for (int i = 1; i < dialoguesArr.Length; i++)
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
            dialogueCounter = 1;
        }
    }
}
