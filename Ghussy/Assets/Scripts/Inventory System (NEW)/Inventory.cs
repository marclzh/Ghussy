using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemNew> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        for (int i = 0; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].item = items[i];
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].item = null;
        }
    }

    public bool AddItem(ItemNew item)
    {
        if (IsFull())
        {
            return false;
        }

        items.Add(item);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(ItemNew item)
    {
        if (items.Remove(item))
        {
            RefreshUI();
            return true;
        }

        return false;
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
    
}
