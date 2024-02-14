using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static DialogManager;

public class TalkableCharacter : Interaction
{
    public override void onClick()
    {
        StartCoroutine(HandleUpdate());
    }

    IEnumerator HandleUpdate()
    {
        UIController.UIControl.HandleDialogBox(false);

        switch (name)
        {
            case "Chair":
                //Mazarine line 1
                yield return DialogManager.Instance.ShowDialog(DialogManager.Character.Mazarine, "Hello, who are you?");

                //Chair's line 1
                yield return DialogManager.Instance.ShowDialog(DialogManager.Character.Chair, "I am chair, nice to meet you.");

                //Mazarine line 2
                yield return DialogManager.Instance.ShowDialog(DialogManager.Character.Mazarine, "Nice to meet you too!");
                break;
            default:
                yield return DialogManager.Instance.ShowDialog(DialogManager.Character.Chair, "fail");
                break;
        }
    }

    public IEnumerator TriggerCutsceneDialog(DialogManager.Character character)
    {
        switch (character)
        {
            case DialogManager.Character.Mazarine:
                yield return DialogManager.Instance.ShowDialog(DialogManager.Character.Mazarine, "Mazarine's cutscene dialog was triggered.");
                break;
            case DialogManager.Character.Chair:
                yield return DialogManager.Instance.ShowDialog(DialogManager.Character.Chair, "Chair's cutscene dialog was triggered.");
                break;
            default:
                yield return DialogManager.Instance.ShowDialog(DialogManager.Character.Chair, "fail");
                break;
        }
    }
}
