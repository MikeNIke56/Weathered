using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : Interaction
{
    [SerializeField] Item itemToOpen;
    [SerializeField] GameObject DoorLogic;
    [SerializeField] AudioSource lockedSFX;
    [SerializeField] AudioSource openDoorSFX;
    [SerializeField] SpriteRenderer doorSR;
    [SerializeField] DoorInteract altDoor;

    public override void onClick()
    {
        if (ItemController.itemInHand == itemToOpen)
        {
            DoorLogic.SetActive(false);
            ItemController.ClearItemInHand();
            doorSR.gameObject.SetActive(false);//Replace with door open sprite instead
            openDoorSFX.Play();
            OpenDoor(false);
            if (altDoor != null)
            {
                altDoor.OpenDoor(true);
            }
        }
        else
        {
            lockedSFX.Play();
            ShortTextController.STControl.AddShortText("Oh, it looks like I can’t get through here yet…", true);
        }
    }

    public void OpenDoor(bool isAltDoor)
    {
        DoorLogic.SetActive(false);
        doorSR.gameObject.SetActive(false);//Replace with door open sprite instead
        if (!isAltDoor)
        {
            ItemController.ClearItemInHand();
            openDoorSFX.Play();
            FindFirstObjectByType<PhoneControl>().NewVoicemail();
        }
    }
}
