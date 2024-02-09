using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterDialog : MonoBehaviour
{
    public GameObject dialogBox;
    PlayerController player;
    public TalkableCharacter targetCharacter;

    public Dialog[] testDialog;


    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
    }
}
