using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FineChinaDoor : Interaction
{
    [SerializeField] string LockedShortText = "It's locked.";
    [SerializeField] Item itemToOpen;
    [SerializeField] DoorScript altDoor;
    [SerializeField] PhoneControl.VoicemailID voicemailID = PhoneControl.VoicemailID.China;

    [SerializeField] GameObject DoorLogic; //Collider to disable
    [SerializeField] AudioSource lockedSFX;
    [SerializeField] AudioSource openDoorSFX;
    [SerializeField] GameObject DoorOpenRoot;
    [SerializeField] GameObject DoorClosedRoot;

    bool HasBeenClickedOn = false;

    public override void onClick()
    {
        if (Progression.HasFinishedToysDolls)
        {
            OpenDoor(false);
        }
        else if (!HasBeenClickedOn)
        {
            HasBeenClickedOn = true;
            StartCoroutine(DoorTalk1());
            lockedSFX.Play();
        }
        else
        {
            StartCoroutine(DoorTalk2());
            lockedSFX.Play();
            ShortTextController.STControl.AddShortText(LockedShortText, true);
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
            if (altDoor != null)
            {
                altDoor.OpenDoor(true);
            }
        }
    }

    IEnumerator DoorTalk1()
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "The door is blocked on the other side.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Can you help?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Not for free.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Money?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "That worthless stuff?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "How about a song.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I don't know how to sing.....");
        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
    IEnumerator DoorTalk2()
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Still waiting for a song~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I don't know how to sing.....");
        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
}
