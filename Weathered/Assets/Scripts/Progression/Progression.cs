using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : MonoBehaviour
{
    public static Progression Prog;

    public static bool ToysDoorSceneTriggered = false; //Celebrity introduced himself
    public static bool HasCheckedOutDesk = false; //Player clicked on computer and phone after celbrity introduced himself
    public static bool HasFinishedToysDolls = false; //Player finished Toys doll task
    public static bool HasFinishedFineChinaDolls = false; //Player finished Fine China doll task
    public static bool HasFinishedDVDDolls = false; //Player finished DVDs doll task
    public static bool HasFinishedCelebrityDolls = false; //Player finished Celebrity doll task
    public static bool HasFixedStairs = false; //Player has entered Taxidermy room and stairs were fixed
    public static bool HasEnteredAuntsRoom = false; //Player has entered the aunt's room
    public static bool HasEnteredMazarinesRoom = false; //Player has entered Mazarine's room

    public enum StoryAreas { Default, AuntsBedroom, MazarinesBedroom};

    [SerializeField] AudioSource MazarineScreamSFX;
    [SerializeField] Animator MazarineBlackout;

    bool IsLoading = true;

    void Start()
    {
        Prog = this;
        StartCoroutine(WaitingForLoadTime());
    }

    IEnumerator WaitingForLoadTime()
    {
        yield return new WaitForSeconds(3f);
        IsLoading = false;
    }

//---- Various Story Dialog ----
    public void StoryAreaEnter(Progression.StoryAreas InArea)
    {
        switch (InArea)
        {
            case Progression.StoryAreas.AuntsBedroom:
                StartCoroutine(AuntsBedroomDialog());
                break;
            case Progression.StoryAreas.MazarinesBedroom:
                StartCoroutine(MazarinesBedroomDialog());
                break;
            default:
                break;
        }
    }

    IEnumerator AuntsBedroomDialog()
    {
        GameManager.PC.moveBlockers["Cutscene"] = true;
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        if (!HasEnteredAuntsRoom)
        {
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "Mazarine wanders into her aunt's bedroom.");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "Only a moment goes by before she looks onto the horrors lying in front of her.");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "*Gasp*");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "She gasps, her adolescent brain unable to put forward an excuse just enough to hide her from the reality.");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "The visceral, gory, unmistakeable scene seered into her eyes creates a pit in her stomach so foul, so dense.");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "What eternity passes by~ she breathelessly lurches and shakes...");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "Another moment passes and the emotions she never had break through the darkness. She-");
            MazarineScreamSFX.Play();
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "No.");
            HasEnteredAuntsRoom = true;
        }
        else
        {
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "No.");
        }

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
        GameManager.PC.moveBlockers["Cutscene"] = false;
    }
    IEnumerator MazarinesBedroomDialog()
    {
        GameManager.PC.moveBlockers["Cutscene"] = true;
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        if (!HasEnteredMazarinesRoom)
        {
            HasEnteredMazarinesRoom = true;
            MazarineBlackout.SetTrigger("AddAlpha");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Mazarine enters her room after a fulfilluing day at the shop.");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "She puts on her cozy, purple pajamas.");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "She carefully undoes her pretty bows.");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "She climbs into her soft bed and yawns another careless yawn.");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "She nods off to sleep, peacefully, thinking that she would love to do this alll again tomorrow.");
            while (true)
            {
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "And the day after that.");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "And the day after that one.");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "And the day after that one, too.");
            }
        }
        else
        {
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "No.");
        }

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
        GameManager.PC.moveBlockers["Cutscene"] = false;
    }

//---- Dolls Story Dialog / Songs ----
    public void ToysDolls()
    {
        HasFinishedToysDolls = true;
        if (!IsLoading)
        {
            StartCoroutine(ToysDollsDialog());
        }
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
        if (!IsLoading)
        {
            StartCoroutine(FineChinaDollsDialog());
        }
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
        if (!IsLoading)
        {
            StartCoroutine(DVDDollsDialog());
        }
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
        if (!IsLoading)
        {
            StartCoroutine(CelebrityDollsDialog());
        }
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
