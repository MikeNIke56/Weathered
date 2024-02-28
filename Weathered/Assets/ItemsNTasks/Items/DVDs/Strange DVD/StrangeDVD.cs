using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeDVD : Item
{
    [SerializeField] StrangeDVDObj ogStrangeDVDObj;
    public void ClickedDVDObject(StrangeDVDObj dvdClicked)
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
        ogStrangeDVDObj.gameObject.SetActive(true);
    }
}
