using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetInteract : Interaction
{
    [SerializeField] Item itemToOpen;
    [SerializeField] GameObject itemToGive;

    public override void onClick()
    {
        if (ItemController.itemInHand == itemToOpen)
        {
            gameObject.SetActive(false);
            ItemController.ClearItemInHand();
            itemToGive.SetActive(true);
        }
        else
            ShortTextController.STControl.AddShortText("It's locked...");
    }
}
