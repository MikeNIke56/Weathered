using System.Collections;
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

    public enum StoryAreas { Default, AuntsBedroom, MazarinesBedroom };

    [SerializeField] AudioSource MazarineScreamSFX;
    [SerializeField] Animator MazarineBlackout;

    [SerializeField] GameObject tutorialBox;
    [SerializeField] GameObject tutorialMan;
    [SerializeField] GameObject toysRoomKey;

    bool IsLoading = true;

    private void Awake()
    {
        Prog = this;
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
        GameManager.PC.moveBlockers["Cutscene"] = true;
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        //Fadeinsong
        BGMManager.BGM.FadeDollsSong(15f, false);
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Hmmm? Now that sounds nice~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Where's that music coming from?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Shhh! You'll talk over it.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "...?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Young Mazarine, loving and bright. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Her parents were proud ‘til that night. ~ ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Alone she’d be saved. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ by aunt who had prayed. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ A ‘daughter’ she took with delight. ~");
        //Fadeoutsong
        BGMManager.BGM.FadeDollsSong(15f, true);
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "...");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "That was a nice enough tune- I could unblock the other door!");
        FindFirstObjectByType<FineChinaDoor>().OpenDoor(false);

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
        GameManager.PC.moveBlockers["Cutscene"] = false;
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
        GameManager.PC.moveBlockers["Cutscene"] = true;
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        //Fadeinsong
        BGMManager.BGM.FadeDollsSong(15f, false);
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Again?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I tend to like this kind of thing.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Young Mazarine, learning to jest. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "It's me again!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Shh!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Along with her aunt, a long test. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ A store almost bare, ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ and things that need care. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ It’s Mazarine doing her best. ~");
        //Fadeoutsong
        BGMManager.BGM.FadeDollsSong(15f, true);
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Hm, hm, hmmm~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I don't like it.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Unsettling songs about you?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "No!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "You're almost famous.~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I'm not...");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "...");
        ItemController.AddItemToHand(FindFirstObjectByType<Fuse>());
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Oh, I found this while they were singing");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "...Thank you.");

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
        GameManager.PC.moveBlockers["Cutscene"] = false;
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
        GameManager.PC.moveBlockers["Cutscene"] = true;
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        //Fadeinsong
        BGMManager.BGM.FadeDollsSong(15f, false);
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I don't want to hear this!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "C'mon~ have an open mind! They're singing for you!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "It isn't like that...");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Young Mazarine, taken by chores. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Away from an aunt she adores. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ A list no small fete, ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ each task she’d complete. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Herself, whom she chose to ignore. ~");
        //Fadeoutsong
        BGMManager.BGM.FadeDollsSong(15f, true);
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Ooo, harsh.");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Can we go?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Just roll with it, Mazarine.~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "...");

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
        GameManager.PC.moveBlockers["Cutscene"] = false;
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
        GameManager.PC.moveBlockers["Cutscene"] = true;
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        //Fadeinsong
        BGMManager.BGM.FadeDollsSong(15f, false);
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Another wonderful performance!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "...");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Mazarine?");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Young Mazarine, working along. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Her actions worth singing a song. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ She dusts and then sweeps. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ A question then creeps. ~");
        //Fadeoutson
        BGMManager.BGM.FadeDollsSong(40f, true);
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.DollSong, "~ Theres Mazarine, knowing what’s right. ~");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Mazarine...");
        FindFirstObjectByType<AuntsDoor>().OpenDoor(false);
        FindFirstObjectByType<CelebAutoGraphs>().falseWall.SetActive(false);

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
        GameManager.PC.moveBlockers["Cutscene"] = false;
    }

    public void HandleReloadedAssets()
    {
        TaskController.taskControl.FindTasks();

        if (TutorialCompleted == true)
        {
            tutorialBox.SetActive(false);
            tutorialMan.SetActive(false);
            TutorialCompleted = true;
            FindFirstObjectByType<PlayerController>().moveBlockers["TutorialDialog"] = false;
            UIController.UIControl.isCamFree = true;

            //in case the tasks doesn't load properly
            FindAnyObjectByType<SortBoxes>().LoadFinishedTask();
            FindAnyObjectByType<KeyEntranceObject>().gameObject.SetActive(false);
        }
        if (ToysDoorSceneTriggered == true)
        {
            FindFirstObjectByType<ToysDoor>().CelebrityIntroRoot.SetActive(true);
            FindFirstObjectByType<ToysDoor>().OpenDoor(false);
            PhoneControl.NewVoicemail(PhoneControl.VoicemailID.Toys);
            FindFirstObjectByType<CIntro>().MoveCeleb();
            toysRoomKey.SetActive(false);
        }
        if (HasCheckedOutDesk == true)
        {

        }
        if (HasFinishedToysDolls == true)
        {
            FindFirstObjectByType<FineChinaDoor>().OpenDoor(false);

        }
        if (HasFinishedFineChinaDolls == true)
        {
            if (FindAnyObjectByType<FixElevator>().currentState != Task.taskState.Completed)
                ItemController.AddItemToHand(FindFirstObjectByType<Fuse>());

        }
        if (HasFinishedDVDDolls == true)
        {

        }
        if (HasFinishedCelebrityDolls == true)
        {

        }

        FindAnyObjectByType<AccessStairs>().HandleStairsLoad(HasFixedStairs);

        if (HasEnteredAuntsRoom == true)
        {

        }
        if (HasEnteredMazarinesRoom == true)
        {

        }

        UIController.UIControl.ToggleInputHandler(false);
        GameManager.GM.HideDeathScreen();
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
