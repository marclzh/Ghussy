using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * This class controls the logic of the NPCs.
 */
public class NPC : MonoBehaviour, INPC
{
    private NPCInteractable npcInteractable;
    // Reference to the dialogues of the NPC.
    [SerializeField] private Transform dialogues;
    // Array to hold the dialogues of the NPC.
    private GameObject[] dialoguesArr;
    // Counters for the dialogues.
    private int dialogueCount;
    private int dialogueCounter;
    private int initDialogueCounter;

    // Reference to current scene
    Scene currScene;

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

        currScene = SceneManager.GetActiveScene();
    }

    // Method to swithc the trigger of the dialogues once played.
    public void SwitchTrigger()
    {
        npcInteractable.SetTrigger(dialoguesArr[dialogueCounter].GetComponent<DialogueTrigger>());
        dialogueCounter++;
        if (dialogueCounter >= dialogueCount)
        {
            dialogueCounter = initDialogueCounter;
        }
    }

    // Setting the dialogue counters.
    public void SetDialogueCounter(int count)
    {
        initDialogueCounter = count;
        dialogueCounter = count;
    }
}
