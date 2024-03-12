using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : MonoBehaviour
{
    public static Progression Prog;

    public static bool ToysDoorSceneTriggered = false; //Celebrity introduced himself
    public static bool HasCheckedOutDesk = false; //Player clicked on computer and phone after celbrity introduced himself
    public static bool HasFinishedToysDolls = false; //Player finished Toys doll task
    public static bool HasFinishedFineChinaDolls = false; //Player finished Toys doll task
    public static bool HasFinishedDVDDolls = false; //Player finished Toys doll task
    public static bool HasFinishedCelebrityDolls = false; //Player finished Toys doll task
    public static bool HasFixedStairs = false; //Player has entered Taxidermy room and stairs were fixed

    void Start()
    {
        Prog = this;
    }
    public void ToysDolls()
    {
        HasFinishedToysDolls = true;
        StartCoroutine(ToysDollsDialog());
    }

    IEnumerator ToysDollsDialog()
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "The dolls sing their tune~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "That was a nice enough tune I could unblock the other door!");
        FindFirstObjectByType<FineChinaDoor>().OpenDoor(false);

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
    public void FineChinaDolls()
    {
        HasFinishedFineChinaDolls = true;
        StartCoroutine(FineChinaDollsDialog());
    }

    IEnumerator FineChinaDollsDialog()
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "The dolls sing their tune~");
        ItemController.AddItemToHand(FindFirstObjectByType<Fuse>());
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Oh, I found this while they were singing");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Thank you...");

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
    public void DVDDolls()
    {
        HasFinishedDVDDolls = true;
        StartCoroutine(DVDDollsDialog());
    }

    IEnumerator DVDDollsDialog()
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "The dolls sing their tune~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I think something changed...");

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
    public void CelebrityDolls()
    {
        HasFinishedCelebrityDolls = true;
        StartCoroutine(CelebrityDollsDialog());
    }

    IEnumerator CelebrityDollsDialog()
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "The dolls sing their tune~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Mazarine...");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Yes?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I feel that I should warn you about going into the next room...");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "What does that mean?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Just... Be careful.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Okay...");

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
}
