using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRewardUI : MonoBehaviour
{
    public void SetUpDisplay()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void CloseDisplay()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
