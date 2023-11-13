using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : Interaction
{
    [SerializeField] CleanFloor cleanFloor;

    public override void onClick()
    {
        if (cleanFloor == null)
        {
            cleanFloor = FindFirstObjectByType<CleanFloor>();
        }

        cleanFloor.ClickedSpot(this);
    }
}
