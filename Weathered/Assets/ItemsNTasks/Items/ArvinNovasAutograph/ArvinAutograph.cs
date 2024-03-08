using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvinAutograph : Item
{
    public ArvinAutographObj arvinAutoObject;
    public void ClickedAutographObject(ArvinAutographObj autoClicked)
    {
        ItemController.AddItemToHand(this);
        autoClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        arvinAutoObject.gameObject.SetActive(true);
    }
}
