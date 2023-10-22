using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusterObject : Interaction
{
    [SerializeField] Duster dusterItem;

    public override void onClick()
    {
        if (dusterItem == null)
        {
            dusterItem = FindFirstObjectByType<Duster>();
        }
        dusterItem.ClickedDusterObject(this);
    }
}
