using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCanOVObj : Interaction
{
    [SerializeField] WateringCan wateringCan;

    public override void onClick()
    {
        if (wateringCan == null)
        {
            wateringCan = FindFirstObjectByType<WateringCan>();
        }
        wateringCan.ClickedWateringCanObject(this);
    }
}
