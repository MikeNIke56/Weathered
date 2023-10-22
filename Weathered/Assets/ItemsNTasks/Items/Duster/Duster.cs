using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duster : Item
{
    [SerializeField] DusterObject originalDusterObject;
    public void ClickedDusterObject(DusterObject dusterClicked)
    {
        ItemController.AddItemToHand(this);
        //Destroy(dusterClicked.gameObject);
        dusterClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        originalDusterObject.gameObject.SetActive(true);
    }
}
