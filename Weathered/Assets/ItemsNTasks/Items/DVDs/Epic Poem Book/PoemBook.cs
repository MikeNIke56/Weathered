using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemBook : Item
{
    [SerializeField] PoemBookObj ogPoemBookOVObj;
    public void ClickedDVDObject(PoemBookObj dvdClicked)
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
        ogPoemBookOVObj.gameObject.SetActive(true);
    }
}
