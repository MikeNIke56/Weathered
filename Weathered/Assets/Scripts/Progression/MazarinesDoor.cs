using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazarinesDoor : Interaction
{
    [SerializeField] PhoneControl.VoicemailID voicemailID = PhoneControl.VoicemailID.None;

    [SerializeField] GameObject DoorLogic; //Collider to disable
    [SerializeField] AudioSource lockedSFX;
    [SerializeField] AudioSource openDoorSFX;
    [SerializeField] GameObject DoorOpenRoot;
    [SerializeField] GameObject DoorClosedRoot;

    public override void onClick()
    {
        if (!Progression.HasEnteredAuntsRoom)
        {
            ShortTextController.STControl.AddShortText("I can't sleep yet, I still have tasks to do!", true);
            lockedSFX.Play();
        }
        else
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        DoorLogic.SetActive(false);
        DoorClosedRoot.SetActive(false);
        DoorOpenRoot.SetActive(true);
        openDoorSFX.Play();
        if (voicemailID != PhoneControl.VoicemailID.None)
        {
            PhoneControl.NewVoicemail(voicemailID);
        }
    }
}
