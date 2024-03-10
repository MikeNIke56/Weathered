using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kettle : Item
{
    public KettleObj kettleObject;

    public void ClickedKettleObject(KettleObj kettleClicked)
    {
        ItemController.AddItemToHand(this);
        kettleClicked.gameObject.SetActive(false);
        kettleObject.kettleWhistle.Stop();
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        kettleObject.gameObject.SetActive(true);
        kettleObject.kettleWhistle.Play();
    }
}
