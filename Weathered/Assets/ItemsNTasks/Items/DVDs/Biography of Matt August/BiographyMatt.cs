using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiographyMatt : Item
{
    [SerializeField] BiographyMattObj ogBioMattObj;

    public void ClickedDVDObject(BiographyMattObj dvdClicked)
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
        ogBioMattObj.gameObject.SetActive(true);
    }
}
