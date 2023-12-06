using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsDropped : Interaction
{
    public override void onClick()
    {
        ItemController.AddItemToHand(FindFirstObjectByType<Scissors>());
        Destroy(gameObject);
    }
}
