using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInteractable : RoomRewardInteractable
{

    public override bool Interact(Interactor interactor)
    {
        // Audio Management
        AudioManager am = FindObjectOfType<AudioManager>();
        am.Play("RoomRewardInteracted");
        am.Play("DoorOpen");
         
        // Raise Event
        base.onRoomRewardInteracted.Raise();

        Destroy(gameObject);
        return true;
    }

    private void Start()
    {
        if (AudioManager.hasInitialised) { AudioManager.Instance.Play("RoomRewardSpawn");  }
  
    }

}
