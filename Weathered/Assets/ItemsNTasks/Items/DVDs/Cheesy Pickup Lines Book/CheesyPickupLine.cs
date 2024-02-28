using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheesyPickupLine : Item
{
    [SerializeField] CheesyPickupLineObj ogCheesyPickupObj;
    public void ClickedDVDObject(CheesyPickupLineObj dvdClicked)
    {
        ItemController.AddItemToHand(this);
        dvdClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        ogCheesyPickupObj.gameObject.SetActive(true);
    }
}
