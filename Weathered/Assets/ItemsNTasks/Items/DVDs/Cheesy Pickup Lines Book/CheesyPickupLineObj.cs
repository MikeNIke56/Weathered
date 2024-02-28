using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheesyPickupLineObj : Interaction
{
    [SerializeField] CheesyPickupLine cheesyPickupLine;

    public override void onClick()
    {
        if (cheesyPickupLine == null)
        {
            cheesyPickupLine = FindFirstObjectByType<CheesyPickupLine>();
        }
        cheesyPickupLine.ClickedDVDObject(this);
    }
}
