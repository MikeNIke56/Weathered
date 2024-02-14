using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerBox : MonoBehaviour
{
    PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "Player":
                StartCoroutine(TalkableCharacter.i.TriggerCutsceneDialog(DialogManager.DialogTriggers.MazarineTestCutScene, this.gameObject));
                break;
            default:
                StartCoroutine(DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Chair, "fail"));
                break;
        }
    }
}
