using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetInteract : Interaction
{
    [SerializeField] Item itemToOpen;

    public override void onClick()
    {
        if (ItemController.itemInHand == itemToOpen)
        {
            gameObject.SetActive(false);
            ItemController.ClearItemInHand();
        }
    }
}
