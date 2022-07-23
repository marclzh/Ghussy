using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image image;
    private ItemNew _item;
    public ItemNew item
    {
        get { return _item;  }
        set
        {
            _item  = value;
            if (_item == null)
            {
                image.enabled = false;
            } 
            else
            {
                image.sprite = _item.icon;
                image.enabled = true;   
            }
        }
    }

    private void OnValidate()
    {
        if (image == null)
        {
            image =  GetComponent<Image>();
        }
    }
}
