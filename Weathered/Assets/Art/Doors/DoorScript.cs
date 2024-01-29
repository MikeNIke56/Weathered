using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interaction
{
    [SerializeField] Item itemToOpen;
    [SerializeField] GameObject DoorLogic; //Collider to disable
    [SerializeField] AudioSource lockedSFX;
    [SerializeField] AudioSource openDoorSFX;
    [SerializeField] GameObject DoorOpenRoot;
    [SerializeField] GameObject DoorClosedRoot;
    [SerializeField] DoorScript altDoor;
    [SerializeField] bool hasVoicemail = true;

    public override void onClick()
    {
        Debug.Log("Hello");
        if (ItemController.itemInHand == itemToOpen)
        {
            DoorLogic.SetActive(false);
            ItemController.ClearItemInHand();
            DoorClosedRoot.SetActive(false);
            DoorOpenRoot.SetActive(true);
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
        DoorClosedRoot.SetActive(false);
        DoorOpenRoot.SetActive(true);
        if (!isAltDoor)
        {
            ItemController.ClearItemInHand();
            openDoorSFX.Play();
            if (hasVoicemail)
            {
                FindFirstObjectByType<PhoneControl>().NewVoicemail();
            }
        }
    }
}
