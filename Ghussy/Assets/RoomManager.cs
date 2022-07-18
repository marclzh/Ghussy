using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private static RoomManager instance;
    [SerializeField] int numOfLevelsCompleted;
    private const int NumOfEachRoomType = 3;
    private const int NumOfRoomTypes = 3;

    // Save Management
    SaveManager saveManager;

    // Verify against build settings
    private int[,] roomPoolSceneIndex = { { 6, 7, 8 }, { 9, 10, 11 }, { 12, 13, 14 } }; // 3 x 3 array (M, E, P) 
    private bool[][] roomCompleted = new bool[3][]; // 3 x 3 array (M, E, P)
    private int[] bossRoomIndex = { 15, 16 };


    // Next Scene To Be Loaded
    public static int nextRoomPositionIndex_First;
    public static int nextRoomPositionIndex_Second;
    public static int nextRoomSceneIndex_First;
    public static int nextRoomSceneIndex_Second;
    public static RoomType nextRoomType_First;
    public static RoomType nextRoomType_Second;

    private void Awake()
    {
        instance = this;
        saveManager = FindObjectOfType<SaveManager>();
        SaveData save = saveManager.activeSave;
 
        roomCompleted[0] = save.roomCompleted_M == null ? new bool[] { false, false, false } : save.roomCompleted_M;
        roomCompleted[1] = save.roomCompleted_E == null ? new bool[] { false, false, false } : save.roomCompleted_E;
        roomCompleted[2] = save.roomCompleted_P == null ? new bool[] { false, false, false } : save.roomCompleted_P;
        numOfLevelsCompleted = save.numOfRoomsCompleted;
    }

    public void GenerateSceneIndexes()
    {
        GetNextRoomSceneIndexes();
    }

    private RoomType[] GetRandomRoomTypes()
    {
        // Choose Random Room Type
        System.Random rnd = new System.Random();
        var roomTypes = Enum.GetValues(typeof(RoomType));

        bool[] unavailableRoomTypes = new bool[] { false, false, false }; // 0 1 2
        int firstRoomTypeIndex = rnd.Next(roomTypes.Length);
        RoomType firstRoom = (RoomType) roomTypes.GetValue(firstRoomTypeIndex);

        // Reroll until room type is available
        while (IsRoomTypeAvailable(firstRoom) == false)
        {
            firstRoomTypeIndex = rnd.Next(roomTypes.Length);
            firstRoom = (RoomType) roomTypes.GetValue(firstRoomTypeIndex);
        }

        unavailableRoomTypes[firstRoomTypeIndex] = true;

        // Ensure duplicate index is not used and room type is available
        int secondRoomTypeIndex = rnd.Next(roomTypes.Length);
        RoomType secondRoom = (RoomType) roomTypes.GetValue(secondRoomTypeIndex);
        while (unavailableRoomTypes[secondRoomTypeIndex] == true || IsRoomTypeAvailable(secondRoom) == false) 
        {
            secondRoomTypeIndex = rnd.Next(roomTypes.Length);
            secondRoom = (RoomType) roomTypes.GetValue(secondRoomTypeIndex);
        }

        RoomType[] rooms = new RoomType[] { firstRoom, secondRoom };

        return rooms;
    }

    private bool IsRoomTypeAvailable(RoomType roomType)
    {
        return GetAvailableRooms(roomType).Count > 0;
    }

    private List<int> GetAvailableRooms(RoomType roomType)
    {
        List<int> rooms = new List<int>();

        switch (roomType)
        {
            case RoomType.MemoryShard:

                for (int i = 0; i < NumOfEachRoomType; i++)
                {
                    if (roomCompleted[0][i] == false)
                    {
                        rooms.Add(i);
                    }
                }

                break;

            case RoomType.Ectoplasm:

                for (int i = 0; i < NumOfEachRoomType; i++)
                {
                    if (roomCompleted[1][i] == false)
                    {
                        rooms.Add(i);
                    }
                }

                break;

            case RoomType.PowerUp:

                for (int i = 0; i < NumOfEachRoomType; i++)
                {
                    if (roomCompleted[2][i] == false)
                    {
                        rooms.Add(i);
                    }

                }

                break;

            default:

                Debug.LogWarning("Invalid Room Type");
                return new List<int>();
        }

        return rooms;
    }

    private int GetSceneIndex(RoomType roomType, int roomIndex)
    {
        if (roomIndex < 0 || roomIndex >= NumOfEachRoomType)
        {
            Debug.LogWarning("Invalid Room Index");
            return -1;
        }

        switch (roomType)
        {
            case RoomType.MemoryShard:
                return roomPoolSceneIndex[0, roomIndex];
            case RoomType.Ectoplasm:
                return roomPoolSceneIndex[1, roomIndex];
            case RoomType.PowerUp:
                return roomPoolSceneIndex[2, roomIndex];
            default:
                Debug.LogWarning("Invalid Room Type");
                return -1;
        }
    }
 
    private int[] GetNextRoomSceneIndexes()
    {
        // Retrieve List of Available Rooms for each room type selected
        RoomType[] rooms = GetRandomRoomTypes();
        List<int> firstRoomIndexList = GetAvailableRooms(rooms[0]);
        List<int> secondRoomIndexList = GetAvailableRooms(rooms[1]);
        
        // Choose random index of list
        System.Random rnd = new System.Random();
        int firstListIndex = rnd.Next(firstRoomIndexList.Count);
        int secondListIndex = rnd.Next(secondRoomIndexList.Count);

        // Retrieve element from list
        int firstRoomIndex = firstRoomIndexList[firstListIndex];
        int secondRoomIndex = secondRoomIndexList[secondListIndex];

        // Get Scene Index
        int[] roomSceneIndexes = new int[rooms.Length];
        roomSceneIndexes[0] = GetSceneIndex(rooms[0], firstRoomIndex);
        roomSceneIndexes[1] = GetSceneIndex(rooms[1], secondRoomIndex);

        // Debug.Log("First Room : " + roomSceneIndexes[0]);
        // Debug.Log("Second Room : " + roomSceneIndexes[1]);

        // Update exposed values
        nextRoomPositionIndex_First = firstRoomIndex;
        nextRoomPositionIndex_Second = secondRoomIndex;
        nextRoomSceneIndex_First = roomSceneIndexes[0];
        nextRoomSceneIndex_Second = roomSceneIndexes[1];
        nextRoomType_First = rooms[0];
        nextRoomType_Second = rooms[1];

        return roomSceneIndexes;
    }

}

public enum RoomType
{
    MemoryShard,
    Ectoplasm,
    PowerUp
}