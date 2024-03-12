using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvinLogic : Interaction
{
    CelebAutoGraphs autoGraphs;
    public int stage = 1;
    public bool isSaved = false;
    public int autosOnGround = 0;

    [SerializeField] ArvinAutograph autograph;

    private void Start()
    {
        autoGraphs = FindAnyObjectByType<CelebAutoGraphs>();
    }
    public override void onClick()
    {
        if(ItemController.itemInHand is BloodyRose)
            autoGraphs.requirementsMet[1] = true;

        StartCoroutine(TalkToArvin(stage));
    }
    IEnumerator TalkToArvin(int stage)
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        switch (stage)
        {
            case 1:
                autoGraphs.OnInProgress();
                if (autoGraphs.requirementsMet[0] == false)
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "clean mirrors so i can stare at myself");
                }
                else
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "clean mirrors so i can stare at myself- ");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "ah, i see youve already done it, here's an autograph ");
                    this.stage = 2;
                    autograph.GiveAutographObject();
                }
                break;
            case 2:
                if (autoGraphs.requirementsMet[1] == false)
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "give me a rose do i can stroke my ego");
                }
                else
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "i need a rose so i can- ");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "ah, i see youve already done it, here's an autograph ");
                    this.stage = 3;
                    autograph.GiveAutographObject();
                }
                break;
            case 3:
                if (autoGraphs.requirementsMet[2] == false)
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "fix my statue");
                }
                else
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "please fix my sta- ");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "ah, i see youve already done it, here's an autograph ");
                    this.stage = 4;
                    autograph.GiveAutographObject();
                }
                break;
            case 4:
                yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "everything is done, thank you");
                break;
            default:
                Debug.Log("fail");
                break;
        }

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
}
