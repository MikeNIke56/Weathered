using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvinAutograph : Item
{
    public ArvinAutographObj arvinAutoObject;
    ArvinLogic arvinLogic;

    private void Start()
    {
        arvinLogic = FindAnyObjectByType<ArvinLogic>();
    }

    public void ClickedAutographObject(ArvinAutographObj autoClicked)
    {
        ItemController.AddItemToHand(this);
        arvinLogic.autosOnGround--;

        if (arvinLogic.autosOnGround <= 0)
            arvinAutoObject.gameObject.SetActive(false);
        else
            arvinAutoObject.gameObject.SetActive(true);    
    }
    public void GiveAutographObject()
    {
        Item prevItem = ItemController.itemInHand;
        ItemController.AddItemToHand(this);

        if (prevItem is ArvinAutograph)
            OnDropped();
    }

    public override void OnDropped()
    {
        arvinLogic.autosOnGround++;
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();

        if(arvinLogic.autosOnGround > 0)
            arvinAutoObject.gameObject.SetActive(true);
        else
            arvinAutoObject.gameObject.SetActive(false);
    }
}
