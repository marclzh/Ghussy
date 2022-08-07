using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPossessionManager : MonoBehaviour
{
    private void Start()
    {
        if (SaveManager.instance.activeSave.shopBossSkeletonPurchased) 
        {
            System.Random rnd = new System.Random();
            int key = rnd.Next(2);

            transform.GetChild(key).gameObject.SetActive(true);
        }
    }
}
