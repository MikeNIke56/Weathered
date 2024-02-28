using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvensHits : Item
{
    [SerializeField] ActionDVDObj ogArvenObj;
    public AudioSource mopSfx;
    public void ClickedDVDObject(ArvensHitsObj dvdClicked)
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
        ogArvenObj.gameObject.SetActive(true);
    }
}
