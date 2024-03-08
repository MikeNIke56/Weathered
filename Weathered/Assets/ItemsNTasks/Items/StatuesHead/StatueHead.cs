using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHead : Item
{
    public StatueHeadObj statueObject;
    public void ClickedStatueObject(StatueHeadObj headClicked)
    {
        ItemController.AddItemToHand(this);
        headClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        statueObject.gameObject.SetActive(true);
    }
}
