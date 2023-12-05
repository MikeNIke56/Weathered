using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetInteract : Interaction
{
    [SerializeField] Item itemToOpen;
    [SerializeField] GameObject itemToGive;
    [SerializeField] Sprite doorOpen;

    public override void onClick()
    {
        if (ItemController.itemInHand == itemToOpen)
        {
            gameObject.SetActive(false);
            ItemController.ClearItemInHand();
            itemToGive.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
        }
        else
            ShortTextController.STControl.AddShortText("It's locked...");
    }
}
