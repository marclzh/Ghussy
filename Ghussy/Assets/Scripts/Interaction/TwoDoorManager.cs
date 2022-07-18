using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDoorManager : MonoBehaviour
{
    private int numOfDoors = 2;
    [SerializeField] GameObject firstDoor;
    [SerializeField] GameObject secondDoor;
    [SerializeField] RoomType firstRoomType;
    [SerializeField] RoomType secondRoomType;

    private void Start()
    {
        firstRoomType = firstDoor.GetComponent<NextLevelInteractable>().nextRoomType;
        secondRoomType = secondDoor.GetComponent<NextLevelInteractable>().nextRoomType;
    }

    public void SetDoorsActive()
    {
        firstDoor.SetActive(true);
        secondDoor.SetActive(true);

        firstRoomType = firstDoor.GetComponent<NextLevelInteractable>().nextRoomType;
        secondRoomType = secondDoor.GetComponent<NextLevelInteractable>().nextRoomType;

        Animator firstDoorAnimator = firstDoor.GetComponentInChildren<Animator>();
        Animator secondDoorAnimator = secondDoor.GetComponentInChildren<Animator>();

        for (int i = 0; i < numOfDoors; i++)
        {
            GameObject currentDoor;
            RoomType currentDoorRoomType;
            Animator currentDoorAnimator;

            if (i == 0)
            {
                currentDoor = firstDoor;
                currentDoorRoomType = firstRoomType;
                currentDoorAnimator = firstDoorAnimator;
            }
            else
            {
                currentDoor = secondDoor;
                currentDoorRoomType = secondRoomType;
                currentDoorAnimator = secondDoorAnimator;
            }

            if (currentDoorRoomType == RoomType.MemoryShard) { currentDoorAnimator.SetTrigger("MemShard"); }
            if (currentDoorRoomType == RoomType.Ectoplasm) { currentDoorAnimator.SetTrigger("Ectoplasm"); }
            if (currentDoorRoomType == RoomType.PowerUp) { currentDoorAnimator.SetTrigger("PowerUp"); }
        }

    }
}
