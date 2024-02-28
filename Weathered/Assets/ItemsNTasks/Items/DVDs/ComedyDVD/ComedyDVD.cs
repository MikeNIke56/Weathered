using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComedyDVD : Item
{
    [SerializeField] ComedyDVDObj ogComedyOVObj;
    public void ClickedDVDObject(ComedyDVDObj dvdClicked)
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
        ogComedyOVObj.gameObject.SetActive(true);
    }
}
