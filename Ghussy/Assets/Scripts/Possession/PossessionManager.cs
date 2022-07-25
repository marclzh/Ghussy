using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionManager : MonoBehaviour
{
    private Transform possessionObject;

    private void Start()
    {
        possessionObject = transform.GetChild(0);
        if (SaveManager.instance.activeSave.shopBossSkeletonPurchased) { possessionObject.gameObject.SetActive(true); }
    }
}
