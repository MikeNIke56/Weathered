using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyRose : Item
{
    public BloodyRoseObj bloodyRoseObject;
    CelebAutoGraphs autoGraphs;

    private void Start()
    {
        autoGraphs = FindAnyObjectByType<CelebAutoGraphs>();
    }
    public void ClickedRoseObject(BloodyRoseObj roseClicked)
    {
        ItemController.AddItemToHand(this);
        //roseClicked.gameObject.SetActive(false);
        autoGraphs.requirementsMet[1] = true;
    }

    public override void OnDropped()
    {
        ClearItem();
        autoGraphs.requirementsMet[1] = false;
    }

    public override void ClearItem()
    {
        base.ClearItem();
        bloodyRoseObject.gameObject.SetActive(true);
    }
}
