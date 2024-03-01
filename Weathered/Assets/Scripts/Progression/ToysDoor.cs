using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToysDoor : Interaction
{
    [SerializeField] Item itemToOpen;
    [SerializeField] DoorScript altDoor;

    [SerializeField] GameObject DoorLogic; //Collider to disable
    [SerializeField] AudioSource lockedSFX;
    [SerializeField] AudioSource openDoorSFX;
    [SerializeField] GameObject DoorOpenRoot;
    [SerializeField] GameObject DoorClosedRoot;

    [SerializeField] GameObject CelebrityIntroRoot;

    public override void onClick()
    {
        if (ItemController.itemInHand == itemToOpen)
        {
            ItemController.ClearItemInHand();
            OpenDoor(false);
            if (altDoor != null)
            {
                altDoor.OpenDoor(true);
            }
            StartCoroutine(IntroduceCelebrity());
        }
        else
        {
            lockedSFX.Play();
            ShortTextController.STControl.AddShortText("It's locked.", true);
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
        }
    }

    IEnumerator IntroduceCelebrity()
    {
        Progression.ToysDoorSceneTriggered = true;
        CelebrityIntroRoot.SetActive(true);

        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Don't yell!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Ahhh!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Tsk. That never works.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "You shouldn't be in here... The store is closed.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I've been around here for a long time, Mazarine. And I have a feeling I'm not leaving anytime soon.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "You know my name?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I do. And your Aunt knows me.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Oh... I guess thats okay then.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "But why were you coming out of a locked room?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Obstacles don't mean much to the amazing me!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Oh! Are you a magician?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Uh... Sure.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Can you do a trick?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I guess I asked for that. Just this once.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Hmmm... Its about time...");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I can predict the future! In a moment, a call from the beyond will echo throughout the hall...");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Thats...");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Just one moment!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "...");
        PhoneControl.NewVoicemail(PhoneControl.VoicemailID.Toys);
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "...!");
        FindFirstObjectByType<CIntro>().MoveCeleb();
        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
}
