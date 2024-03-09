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

        if (arvinLogic.stage == 1)
            arvinAutoObject.gameObject.SetActive(false);
        else if (arvinLogic.stage >= 2)
        {
            if (arvinLogic.autographsNotUsed <= 0)
                arvinAutoObject.gameObject.SetActive(false);
        }       
    }
    public void GiveAutographObject()
    {
        ItemController.AddItemToHand(this);
        if (arvinLogic.autographsNotUsed <= 0)
            arvinAutoObject.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();

        if(arvinLogic.autographsNotUsed > 0 || arvinLogic.isSaved == true)
            arvinAutoObject.gameObject.SetActive(true);
        else
            arvinAutoObject.gameObject.SetActive(false);
    }

    public void SaveAutoGraph()
    {
        arvinAutoObject.gameObject.SetActive(true);
    }
}
