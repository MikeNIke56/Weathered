using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuntsDoor : Interaction
{
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
        OpenDoor(false);
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
            if (altDoor != null)
            {
                altDoor.OpenDoor(true);
            }
        }
    }
}
