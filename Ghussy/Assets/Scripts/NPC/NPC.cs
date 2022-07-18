using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour, INPC
{
    private NPCInteractable npcInteractable;
    [SerializeField] private Transform dialogues;
    private GameObject[] dialoguesArr;
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
