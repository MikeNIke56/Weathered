using System;
using System.Collections;
using UnityEngine;

public class PhoneControl : Interaction
{
    [SerializeField] AudioSource ringingSFX;
    static bool isAnswerable = false;
    static PhoneControl PC;
    public enum VoicemailID { None, Toys, China, DVDs, Taxidermy, Celebrity, Aunts, Mazarines };
    static VoicemailID CurrentVID = VoicemailID.Toys;
    private void Start()
    {
        PC = FindFirstObjectByType<PhoneControl>();
    }

    public static void NewVoicemail(VoicemailID VID)
    {
        isAnswerable = true;
        CurrentVID = VID;
        try
        {
            PC.ringingSFX.Play();
            PC.GetComponent<Animator>().SetBool("isBeeping", true);
        }
        catch (Exception e)
        {
            Debug.Log("no phone ring");
            Debug.Log(e);
        }
    }
    public override void onClick()
    {
        if (isAnswerable)
        {
            StartCoroutine(VoicemailDialog());
            isAnswerable = false;
            PC.GetComponent<Animator>().SetBool("isBeeping", false);
        }
    }

    IEnumerator VoicemailDialog()
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        switch (CurrentVID)
        {
            case VoicemailID.Toys:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "My dear Mazarine...");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "How could you miss my call?");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "You couldn't have been helping out another customer, could you?");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "I have so much to tell you!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, ".......................................");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "Mazarine writes the helpful information down in her journal~~");

                TaskBatch1();

                break;
            case VoicemailID.China:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "You are in the china room section now");
                break;
            default:
                break;

        }

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }

    void TaskBatch1()
    {
        FindAnyObjectByType<ArrangeDolls>().hintGiven = true;
        FindAnyObjectByType<ReplaceLightBulb>().hintGiven = true;
        FindAnyObjectByType<FixDolls>().hintGiven = true;
        FindAnyObjectByType<MusicBox>().hintGiven = true;
        FindAnyObjectByType<CleanFloor>().hintGiven = true;
        FindAnyObjectByType<ArrangeSnowglobes>().hintGiven = true;
        FindAnyObjectByType<SortToys>().hintGiven = true;
        FindAnyObjectByType<ArrangeDolls>().hasBeenDisc = true;
        FindAnyObjectByType<ReplaceLightBulb>().hasBeenDisc = true;
        FindAnyObjectByType<FixDolls>().hasBeenDisc = true;
        FindAnyObjectByType<MusicBox>().hasBeenDisc = true;
        FindAnyObjectByType<CleanFloor>().hasBeenDisc = true;
        FindAnyObjectByType<ArrangeSnowglobes>().hasBeenDisc = true;
        FindAnyObjectByType<SortToys>().hasBeenDisc = true;
        TaskController.taskControl.SetPage(0);
    }
}
