using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Interactable class that handles the logic of transitioning the player to the next scene
 */
public class NextLevelInteractable : MonoBehaviour, IInteractable
{
    // Scene Management
    [SerializeField] private int nextSceneIndex;
    [SerializeField] private int nextRoomPositionIndex;
    [SerializeField] public RoomType nextRoomType;
    [SerializeField] public NextLevelType nextLevelType; 

    // IInteractable Fields
    [SerializeField] private string prompt;

    // Returns prompt dialogue
    public string InteractionPrompt => prompt;

    private void Awake()
    {
        switch (nextLevelType)
        {
            case NextLevelType.FirstRoom:

                nextSceneIndex = RoomManager.nextRoomSceneIndex_First;
                nextRoomPositionIndex = RoomManager.nextRoomPositionIndex_First;
                nextRoomType = RoomManager.nextRoomType_First;
                break;

           case NextLevelType.SecondRoom:

                nextSceneIndex = RoomManager.nextRoomSceneIndex_Second;
                nextRoomPositionIndex = RoomManager.nextRoomPositionIndex_Second;
                nextRoomType = RoomManager.nextRoomType_Second;
                break;

            case NextLevelType.Boss:
                nextSceneIndex = RoomManager.bossIndex;
                break;
            
            default:
                nextRoomPositionIndex = -1; 
                nextLevelType = NextLevelType.Others;
                break;
                
        }
    }

    public bool Interact(Interactor interactor)
    {
        // Retrieve Save
        SaveData save = SaveManager.instance.activeSave;

        // If next level is within the main game
        if (nextLevelType == NextLevelType.FirstRoom || nextLevelType == NextLevelType.SecondRoom)
        {
            // Flag Room as Selected
            int roomTypeIndex = (int) nextRoomType;
            if (roomTypeIndex == 0) { save.roomCompleted_M[nextRoomPositionIndex] = true; };
            if (roomTypeIndex == 1) { save.roomCompleted_E[nextRoomPositionIndex] = true; };
            if (roomTypeIndex == 2) { save.roomCompleted_P[nextRoomPositionIndex] = true; };
            save.lastRoomInteractedTypeIndex = roomTypeIndex;
            save.numOfRoomsCompleted++;
        }

        // Reset Stored Player Position
        save.playerPos = new float[] { 0f, 0f, 0f };

        // Update saved scene and saves game
        save.savePointSceneIndex = nextSceneIndex;
        SaveManager.instance.SaveGame();

        // Loads next Scene
        SceneManager.LoadSceneAsync(nextSceneIndex);

        return true;
    }
}

/**
 * Enum of the next level types
 */
public enum NextLevelType
{
    FirstRoom,
    SecondRoom,
    Boss,
    Others
}
