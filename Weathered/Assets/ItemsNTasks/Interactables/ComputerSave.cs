using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class ComputerSave : Interaction
{
    PlayerController player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>(FindObjectsInactive.Include);
    }
    public override void onClick()
    {
        Progression.Prog.HasCheckedOutDesk = true;
        UIController.UIControl.OpenSaveUI();
        player.state = GameState.Menu;
    }
}
