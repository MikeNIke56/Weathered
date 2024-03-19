using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrityInteractionScript : Interaction
{
    public enum CPoints {Default, DeskIntro, ChinaDoorHint};
    public CPoints CurrentPoint = CPoints.Default;
    int talkedCount = 0;

    public override void onClick()
    {
        StartCoroutine(TalkToCelebrity());
    }

    IEnumerator TalkToCelebrity()
    {
        if (CurrentPoint != CPoints.Default)
        {
            UIController.UIControl.OpenDialog();
            DialogManager.Instance.OpenDialog();
        }
        switch (CurrentPoint)
        {
            case CPoints.DeskIntro:
                if (!Progression.HasCheckedOutDesk && talkedCount == 0)
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "So? Amazed by me?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "That wasn't a very good trick...");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "C'mon, that had to have at least suprised you.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "No... My aunt always calls to check up on me.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Right now?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "All night...");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Sounds excessive.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "What's that mean, again?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "A lot, but also too much.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Oh... I don't know... She always has something useful to say.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "But?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Hehe.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "...");
                    talkedCount = 1;
                }
                else if (!Progression.HasCheckedOutDesk && talkedCount == 1)
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Wanna see another trick?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Yes, please!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Too bad. My last trick didn't get nearly enough applause and now I am all out of good deeds.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Meanie.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I don't work for free anymore. I'm retired~~");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I work for free!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Shocking.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Hey!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Well, I mean that wasn't too out of line!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "No! What if I showed you something cool? Would you show me your trick then?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Oh, yeah, sure.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "The computer!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Yes?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "It helps me keep track of the entire store! Here, I'll show you.");
                    talkedCount = 2;
                }
                else if (!Progression.HasCheckedOutDesk && talkedCount == 2)
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I didn't see it.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "What?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "The computer.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "But... It's right there?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "It requires a password.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "My aunt told me not to tell it to anyone...");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "You could just login yourself and show me.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Oh, right...");
                }
                else if (Progression.HasCheckedOutDesk)
                {
                    talkedCount = 0;
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "So, will you show me another magic trick now?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Close your eyes.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Okay!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Now imagine yourself in other world...");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "What's that meant to do?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I-- don't know? Usually it just works.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "What does?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Just try it out. (Spacebar)");
                }

                break;
            default:
                break;
        }

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
}
