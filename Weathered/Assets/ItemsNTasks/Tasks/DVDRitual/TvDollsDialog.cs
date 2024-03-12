using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogManager;
using UnityEngine.TextCore.Text;

public class TvDollsDialog : Interaction
{
    public int dollnum;
    public override void onClick()
    {
        StartCoroutine(DollTalk(dollnum));
    }

    IEnumerator DollTalk(int dollnum)
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();
        switch (dollnum)
        {
            case 0:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll1, "HeHeHe~");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll1, "Tennesee!");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll1, "I want a movie I can laugh at! But I don't want it to be funny.");
                break;
            case 1:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll2, "I need something to wake me up~");
                break;
            case 2:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll3, "Oh... I don't know...");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll3, "I might like it if it has a cool movie star...");
                break;
            default:
                Debug.Log("fail");
                break;
        }
        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
}
