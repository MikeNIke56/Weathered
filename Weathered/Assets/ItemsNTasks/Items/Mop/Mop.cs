using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : Item
{
    [SerializeField] MopObject originalMopObject;
    public void ClickedMopObject(MopObject  mopClicked)
    {
        ItemController.AddItemToHand(this);
        //Destroy(dusterClicked.gameObject);
        mopClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        originalMopObject.gameObject.SetActive(true);
    }
}
