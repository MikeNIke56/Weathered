using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDVDObject : Interaction
{
    public override void onClick()
    {
        ItemController.AddItemToHand(FindFirstObjectByType<KeyDVD>());
        Destroy(gameObject);
    }
}
