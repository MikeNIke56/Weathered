using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Item
{
    public WaterCanOVObj wateringCanObject;
    public void ClickedWateringCanObject(WaterCanOVObj canClicked)
    {
        ItemController.AddItemToHand(this);
        canClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        wateringCanObject.gameObject.SetActive(true);
    }
}
