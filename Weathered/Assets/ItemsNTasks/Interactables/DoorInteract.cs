using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : Interaction
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
