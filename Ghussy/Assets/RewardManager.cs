using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public void SpawnReward() 
    {
        var roomReward = transform.GetChild(0);
        if (roomReward != null)
        {
            roomReward.gameObject.SetActive(true);
        }
            
    }
}
