using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvinAutograph : Item
{
    public ArvinAutographObj arvinAutoObject;
    ArvinLogic arvinLogic;

    private void Start()
    {
        arvinLogic =FindAnyObjectByType<ArvinLogic>();
    }

    public void ClickedAutographObject(ArvinAutographObj autoClicked)
    {
        ItemController.AddItemToHand(this);
        autoClicked.gameObject.SetActive(false);
    }
    public void GiveAutographObject()
    {
        ItemController.AddItemToHand(this);
        arvinAutoObject.gameObject.SetActive(false);
        arvinLogic.autoGraphGiven = true;
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();

        if(arvinLogic.autoGraphGiven == true)
            arvinAutoObject.gameObject.SetActive(true);
        else
            arvinAutoObject.gameObject.SetActive(false);
    }
}
