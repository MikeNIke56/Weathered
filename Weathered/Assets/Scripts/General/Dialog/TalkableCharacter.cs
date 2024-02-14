using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static DialogManager;

public class TalkableCharacter : Interaction
{
    PlayerController player;
    public bool isDone = false;

    public override void onClick()
    {
        StartCoroutine(HandleUpdate());
    }

    public static TalkableCharacter i { get; private set; }
    private void Awake()
    {
        i = this;
        player = FindAnyObjectByType<PlayerController>();
    }

    IEnumerator HandleUpdate()
    {
        UIController.UIControl.HandleDialogBox(false);

        switch (name)
        {
            case "Chair":
                //Mazarine line 1
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Hello, who are you?");

                //Chair's line 1
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Chair, "I am chair, nice to meet you.");

                //Mazarine line 2
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Nice to meet you too!");
                break;
            default:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Chair, "fail");
                break;
        }
    }

    public IEnumerator TriggerCutsceneDialog(DialogManager.DialogTriggers character, GameObject triggerBox)
    {
        switch (character)
        {
            case DialogManager.DialogTriggers.MazarineTestCutScene:
                isDone = false;
                player.moveBlockers["CharacterDialog"] = true;
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Mazarine's cutscene dialog was triggered.");
                player.moveBlockers["CharacterDialog"] = false;
                triggerBox.SetActive(false);
                break;
            case DialogManager.DialogTriggers.Chair:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Chair, "Chair's cutscene dialog was triggered.");
                break;
            default:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Chair, "fail");
                break;
        }
    }
}
