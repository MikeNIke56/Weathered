using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileObj : Interaction
{
    public Item item;
    DVDRitual ritual;

    private void Start()
    {
        ritual = FindAnyObjectByType<DVDRitual>();
    }

    public override void onClick()
    {
        ItemController.AddItemToHand(item);
        ritual.OnInProgress();
    }
}
