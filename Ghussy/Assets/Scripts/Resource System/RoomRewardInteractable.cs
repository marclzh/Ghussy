using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRewardInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] VoidEvent onRoomRewardInteracted;
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        AudioManager am = FindObjectOfType<AudioManager>();
        am.Play("RoomRewardInteracted");
        am.Play("DoorOpen");
        
        onRoomRewardInteracted.Raise();
        Destroy(gameObject);
        return true;
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("RoomRewardSpawn");
    }

}
