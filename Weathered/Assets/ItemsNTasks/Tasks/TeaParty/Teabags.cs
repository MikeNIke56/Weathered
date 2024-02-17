using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teabags : Item
{
    public TeabagsObj teaBagsObject;
    public void ClickedTeaBagObject(TeabagsObj teaBagClicked)
    {
        ItemController.AddItemToHand(this);
        teaBagClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        teaBagsObject.gameObject.SetActive(true);
    }
}
