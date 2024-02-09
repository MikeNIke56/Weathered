using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkableCharacter : Interaction
{
    public bool triggered = false;
    public bool talksFirst = false;
    PlayerController player;
    CharacterDialog characterDialog;
    public GameObject dialogBox;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
        characterDialog = FindFirstObjectByType<CharacterDialog>();
    }

    private void Update()
    {
        
    }

    public override void onClick()
    {
        characterDialog.targetCharacter = this;
        StartCoroutine(HandleUpdate());
    }

    IEnumerator HandleUpdate()
    {
        //dialogBox.SetActive(true);
        UIController.UIControl.HandleDialogBox(false);

        //Mazarine line 1
        yield return DialogManager.Instance.ShowDialog(characterDialog.testDialog, 0);

        //this object's line 1
        yield return DialogManager.Instance.ShowDialog("I am another person talking.");

        //Mazarine line 2
        yield return DialogManager.Instance.ShowDialog(characterDialog.testDialog, 1);
        UIController.UIControl.HandleDialogBox(true);
        //dialogBox.SetActive(false);
        yield return null;
    }
}
