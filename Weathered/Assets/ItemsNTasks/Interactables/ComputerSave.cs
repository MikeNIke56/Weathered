using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class ComputerSave : Interaction
{
    [SerializeField] GameObject saveUI;
    PlayerController player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>(FindObjectsInactive.Include);
    }
    public override void onClick()
    {
        saveUI.SetActive(true);
        player.state = GameState.Menu;
    }
}
