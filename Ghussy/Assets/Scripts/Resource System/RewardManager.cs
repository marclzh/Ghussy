using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] RoomType roomType;
    private int numOfLevelsCompleted;
    private int maxLevels = 5; // Hard Coded based on edge case

    private void Start()
    {
        numOfLevelsCompleted = SaveManager.instance.activeSave.numOfRoomsCompleted;
        if (numOfLevelsCompleted > maxLevels) { SpawnReward(); }
        
    }

    public void SpawnReward()
    {
        Transform roomReward;

        if (numOfLevelsCompleted <= maxLevels)
        {
            roomReward = transform.GetChild(0);
        }
        else
        { 
            roomType = (RoomType)SaveManager.instance.activeSave.lastRoomInteractedTypeIndex;
            int roomTypeIndex = (int)roomType;
            roomReward = transform.GetChild(roomTypeIndex);
        }

        if (roomReward != null)
        {
            roomReward.gameObject.SetActive(true);
            Animator roomRewardAnimator = roomReward.GetComponent<Animator>();

            if (roomRewardAnimator != null)
            {
                switch (roomType)
                {
                    case RoomType.Ectoplasm:
                        roomRewardAnimator.SetTrigger("Ectoplasm");
                        break;
                    case RoomType.MemoryShard:
                        roomRewardAnimator.SetTrigger("MemShard");
                        break;
                    case RoomType.PowerUp:
                        roomRewardAnimator.SetTrigger("PowerUp");
                        break;
                }

            }
        }
    }
    
}
