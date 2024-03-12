using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Interaction
{
    [SerializeField] bool isLocked = false;
    [SerializeField] string LockedShortText = "It's locked.";
    [SerializeField] Item itemToOpen;
    [SerializeField] DoorScript altDoor;
    [SerializeField] PhoneControl.VoicemailID voicemailID = PhoneControl.VoicemailID.None;

    [SerializeField] GameObject DoorLogic; //Collider to disable
    [SerializeField] AudioSource lockedSFX;
    [SerializeField] AudioSource openDoorSFX;
    [SerializeField] GameObject DoorOpenRoot;
    [SerializeField] GameObject DoorClosedRoot;

    public override void onClick()
    {
        if (isLocked)
        {
            lockedSFX.Play();
            ShortTextController.STControl.AddShortText(LockedShortText, true);
        }
        else if (itemToOpen != null && ItemController.itemInHand == itemToOpen)
        {
            ItemController.ClearItemInHand();
            OpenDoor(false);
            if (altDoor != null)
            {
                altDoor.OpenDoor(true);
            }
        }
        else if (itemToOpen == null && !isLocked)
        {
            OpenDoor(false);
            if (altDoor != null)
            {
                altDoor.OpenDoor(true);
            }
        }
        else
        {
            lockedSFX.Play();
            ShortTextController.STControl.AddShortText("Oh, I can’t get through here yet…", true);
        }
    }

    public void OpenDoor(bool isAltDoor)
    {
        DoorLogic.SetActive(false);
        DoorClosedRoot.SetActive(false);
        DoorOpenRoot.SetActive(true);
        if (!isAltDoor)
        {
            openDoorSFX.Play();
            if (voicemailID != PhoneControl.VoicemailID.None)
            {
                PhoneControl.NewVoicemail(voicemailID);
            }
        }
    }
}
