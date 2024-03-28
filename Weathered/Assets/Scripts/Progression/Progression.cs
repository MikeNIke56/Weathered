using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class Progression : MonoBehaviour, ISavable
{
    public static Progression Prog;

    public bool TutorialCompleted = false; //Player finished tutorial
    public bool ToysDoorSceneTriggered = false; //Celebrity introduced himself
    public bool HasCheckedOutDesk = false; //Player clicked on computer and phone after celebrity introduced himself
    public bool HasFinishedToysDolls = false; //Player finished Toys doll task
    public bool HasFinishedFineChinaDolls = false; //Player finished Fine China doll task
    public bool HasFinishedDVDDolls = false; //Player finished DVDs doll task
    public bool HasFinishedCelebrityDolls = false; //Player finished Celebrity doll task
    public bool HasFixedStairs = false; //Player has entered Taxidermy room and stairs were fixed
    public bool HasEnteredAuntsRoom = false; //Player has entered the aunt's room
    public bool HasEnteredMazarinesRoom = false; //Player has entered Mazarine's room

    public enum StoryAreas { Default, AuntsBedroom, MazarinesBedroom};

    [SerializeField] AudioSource MazarineScreamSFX;
    [SerializeField] Animator MazarineBlackout;

    [SerializeField] GameObject tutorialBox;
    [SerializeField] GameObject tutorialMan;

    bool IsLoading = true;

    private void Awake()
    {
        Prog = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        StartCoroutine(WaitingForLoadTime());
    }

    IEnumerator WaitingForLoadTime()
    {
        yield return new WaitForSeconds(2f);
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
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "She nods off to sleep, peacefully, thinking that she would love to do this all again tomorrow.");
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

    public void HandleReloadedAssets()
    {
        if (TutorialCompleted == true)
        {
            tutorialBox.SetActive(false);
            tutorialMan.SetActive(false);
            TutorialCompleted = true;
            FindFirstObjectByType<PlayerController>().moveBlockers["TutorialDialog"] = false;

            //in case the tasks doesn't load properly
            FindAnyObjectByType<DustCobwebs>().LoadFinishedTask();
            FindAnyObjectByType<SortBoxes>().LoadFinishedTask();
            FindAnyObjectByType<KeyEntranceObject>().gameObject.SetActive(false);
        }
        if (ToysDoorSceneTriggered == true)
        {
            FindFirstObjectByType<ToysDoor>().CelebrityIntroRoot.SetActive(true);
            FindFirstObjectByType<ToysDoor>().OpenDoor(false);
            PhoneControl.NewVoicemail(PhoneControl.VoicemailID.Toys);
            FindFirstObjectByType<CIntro>().MoveCeleb();
            ToysDoorSceneTriggered = true;
        }
        if (HasCheckedOutDesk == true)
        {
            HasCheckedOutDesk = true;
        }
        if (HasFinishedToysDolls == true)
        {
            FindFirstObjectByType<FineChinaDoor>().OpenDoor(false);
            HasFinishedToysDolls = true;
        }
        if (HasFinishedFineChinaDolls == true)
        {
            if(FindAnyObjectByType<FixElevator>().currentState != Task.taskState.Completed)
                ItemController.AddItemToHand(FindFirstObjectByType<Fuse>());

            HasFinishedFineChinaDolls = true;
        }
        if (HasFinishedDVDDolls == true)
        {
            HasFinishedDVDDolls = true;
        }
        if (HasFinishedCelebrityDolls == true)
        {
            HasFinishedCelebrityDolls = true;
        }

        FindAnyObjectByType<AccessStairs>().HandleStairsLoad(HasFixedStairs);

        if (HasEnteredAuntsRoom == true)
        {
            HasEnteredAuntsRoom = true;

        }
        if (HasEnteredMazarinesRoom == true)
        {
            HasEnteredMazarinesRoom = true;
        }
    }

    public GameSaveData GetGameSaveData()
    {
        var saveData = new GameSaveData()
        {
            TutorialCompleted = TutorialCompleted,
            ToysDoorSceneTriggered = ToysDoorSceneTriggered,
            HasCheckedOutDesk = HasCheckedOutDesk,
            HasFinishedToysDolls = HasFinishedToysDolls,
            HasFinishedFineChinaDolls = HasFinishedFineChinaDolls,
            HasFinishedDVDDolls = HasFinishedDVDDolls,
            HasFinishedCelebrityDolls = HasFinishedCelebrityDolls,
            HasFixedStairs = HasFixedStairs,
            HasEnteredAuntsRoom = HasEnteredAuntsRoom,
            HasEnteredMazarinesRoom = HasEnteredMazarinesRoom
        };
        return saveData;
    }

    void SetProgression(GameSaveData saveData)
    {
        TutorialCompleted = saveData.TutorialCompleted;
        ToysDoorSceneTriggered = saveData.ToysDoorSceneTriggered;
        HasCheckedOutDesk = saveData.HasCheckedOutDesk;
        HasFinishedToysDolls = saveData.HasFinishedToysDolls;
        HasFinishedFineChinaDolls = saveData.HasFinishedFineChinaDolls;
        HasFinishedDVDDolls = saveData.HasFinishedDVDDolls;
        HasFinishedCelebrityDolls = saveData.HasFinishedCelebrityDolls;
        HasFixedStairs = saveData.HasFixedStairs;
        HasEnteredAuntsRoom = saveData.HasEnteredAuntsRoom;
        HasEnteredMazarinesRoom = saveData.HasEnteredMazarinesRoom;
    }

    public object CaptureState()
    {
        return GetGameSaveData();
    }

    public void RestoreState(object state)
    {
        var saveData = state as GameSaveData;
        SetProgression(saveData);
    }
}

[System.Serializable]
public class GameSaveData
{
    public bool TutorialCompleted = false;
    public bool ToysDoorSceneTriggered = false;
    public bool HasCheckedOutDesk = false;
    public bool HasFinishedToysDolls = false;
    public bool HasFinishedFineChinaDolls = false;
    public bool HasFinishedDVDDolls = false;
    public bool HasFinishedCelebrityDolls = false;
    public bool HasFixedStairs = false;
    public bool HasEnteredAuntsRoom = false;
    public bool HasEnteredMazarinesRoom = false;
}
