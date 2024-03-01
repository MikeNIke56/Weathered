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
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll1, "I want this type of movie");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Action movie");
                break;
            case 1:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll2, "I want this type of movie");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Sad movie");
                break;
            case 2:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll3, "I want this type of movie");
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Comedy movie");
                break;
            default:
                Debug.Log("fail");
                break;
        }
        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
}
