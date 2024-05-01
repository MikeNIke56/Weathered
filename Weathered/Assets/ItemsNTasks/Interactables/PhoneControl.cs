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
        GameManager.PC.moveBlockers["Cutscene"] = true;
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
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "My sweet Mazarine...");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "You missed another call.");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "Do you not want to talk with your dear Aunty?");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "I have so much to tell you!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, ".......................................");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "Mazarine writes the helpful information down in her journal~~");
                break;
            case VoicemailID.DVDs:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "My industrious Mazarine...");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "I miss hearing your voice, Mazarine...-");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "Why won’t you talk to me...");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "I have so much to tell you!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, ".......................................");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "Mazarine writes the helpful information down in her journal~~");
                break;
            case VoicemailID.Taxidermy:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "My delicate Mazarine...");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "This is all your fault, Mazarine!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "You knew I would get MAD but you still ignored me!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "I have so much to tell you!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, ".......................................");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "Mazarine writes the helpful information down in her journal~~");
                break;
            case VoicemailID.Celebrity:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "Ohhhh...");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "My little, butterfly Mazarine...");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "If only I could clip your wings so that you would NEVER leave me.");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "I have so much to tell you!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, ".......................................");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Narrator, "Mazarine writes the helpful information down in her journal~~");
                break;
            case VoicemailID.Aunts:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "MY VILE MAZARINE!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "YOU ARE JUST LIKE YOUR PARENTS, MAZARINE!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "NEVER REACHING OUT!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "NEVER HELPING ME!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "I DID THIS FOR US, MAZARINE!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "WHY CAN’T YOU SEE THAT?");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "WHY DON’T YOU CARE?");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, ".......................................");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Mazarine writes the helpful information down in her journal~~");
                break;
            case VoicemailID.Mazarines:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "My Mazarine,");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "Just like her parents.");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "Her cold, lifeless parents.");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "You never listened, Mazarine.");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "If only you did as you were told.");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "If only you answered the phone.");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Aunt, "If only you didn’t-");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, ".......................................");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Mazarine writhes the. help informat i on . in . journy~~");
                break;
            default:
                break;

        }

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
        GameManager.PC.moveBlockers["Cutscene"] = false;
    }

    void TaskBatch1()
    {
        FindAnyObjectByType<ArrangeDolls>().hintGiven = true;
        FindAnyObjectByType<ReplaceLightBulb>().hintGiven = true;
        FindAnyObjectByType<FixDolls>().hintGiven = true;
        FindAnyObjectByType<MusicBox>().hintGiven = true;
        FindAnyObjectByType<CleanFloor>().hintGiven = true;
        FindAnyObjectByType<ArrangeSnowglobes>().hintGiven = true;
        FindAnyObjectByType<ArrangeDolls>().hasBeenDisc = true;
        FindAnyObjectByType<ReplaceLightBulb>().hasBeenDisc = true;
        FindAnyObjectByType<FixDolls>().hasBeenDisc = true;
        FindAnyObjectByType<MusicBox>().hasBeenDisc = true;
        FindAnyObjectByType<CleanFloor>().hasBeenDisc = true;
        FindAnyObjectByType<ArrangeSnowglobes>().hasBeenDisc = true;
        TaskController.taskControl.SetPage(0);
    }
}
