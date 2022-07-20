using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRewardUI : MonoBehaviour
{
    [SerializeField] PlayerController controller;

    public void SetUpDisplay()
    {

         controller.ActionMapMenuChange();
         transform.GetChild(0).gameObject.SetActive(true);
      
       
    }

    public void CloseDisplay()
    {
        controller.ActionMapPlayerChange();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
