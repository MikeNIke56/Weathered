using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : Item
{
    [SerializeField] BoxOfLightbulbs lightbulbObj;
    public void ClickedLightbulbObject(Lightbulb lightbulb)
    {
        ItemController.AddItemToHand(lightbulb);
        lightbulb.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        lightbulbObj.gameObject.SetActive(true);
    }
}
