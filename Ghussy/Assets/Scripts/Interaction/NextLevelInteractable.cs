using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] int nextSceneIndex;
    [SerializeField] int nextRoomPositionIndex;
    [SerializeField] public RoomType nextRoomType;
    [SerializeField] NextLevelType nextLevelType; 

    // Save Management
    SaveManager saveManager;

    // Interaction Fields
    [SerializeField] private string prompt;

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
        saveManager = FindObjectOfType<SaveManager>();
    }

    public bool Interact(Interactor interactor)
    {
        SaveData save = saveManager.activeSave;

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
        saveManager.SaveGame();

        // Loads next Scene
        SceneManager.LoadSceneAsync(nextSceneIndex);

        return true;
    }

}

public enum NextLevelType
{
    FirstRoom,
    SecondRoom,
    Boss,
    Others
}
