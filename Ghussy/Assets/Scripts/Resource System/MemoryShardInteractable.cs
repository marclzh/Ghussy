using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryShardInteractable : RoomRewardInteractable
{
    [SerializeField] private GameObject memoryShardPrefab;

    public override bool Interact(Interactor interactor)
    {
        // Audio Management
        AudioManager am = FindObjectOfType<AudioManager>();
        am.Play("RoomRewardInteracted");
        am.Play("DoorOpen");

       // Spawn Memory Shard
       memoryShardPrefab.GetComponent<MemoryShard>().source = MemoryShardSource.Reward;
       Instantiate(memoryShardPrefab, transform.position + new Vector3(0, Random.Range(0, .32f)), Quaternion.identity);
          
        // Raise Event
        base.onRoomRewardInteracted.Raise();

        Destroy(gameObject);
        return true;
    }

    private void Start()
    {
        if (AudioManager.hasInitialised) { AudioManager.Instance.Play("RoomRewardSpawn"); }
    }

}
