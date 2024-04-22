using System;
using UnityEngine;

public class PileObj : Interaction
{
    Item item;
    DVDRitual ritual;

    public enum Pile { ArvinsHits, CheesyPickupLine, ActionDVD, BiographyMatt, PoemBook, ComedyDVD }; //states that the player can be in
    public Pile pileItem;

    private void Start()
    {
        ritual = FindAnyObjectByType<DVDRitual>();
        SearchItem();
    }

    public override void onClick()
    {
        SearchItem();
        ItemController.AddItemToHand(item);
        ritual.OnInProgress();
    }

    public void SearchItem()
    {
        switch (pileItem)
        {
            case Pile.ArvinsHits:
                item = FindAnyObjectByType<ArvensHits>();
                FindAnyObjectByType<ArvensHits>().FindItem();
                break;
            case Pile.CheesyPickupLine:
                item = FindAnyObjectByType<CheesyPickupLine>();
                FindAnyObjectByType<CheesyPickupLine>().FindItem();
                break;
            case Pile.ActionDVD:
                item = FindAnyObjectByType<ActionDVD>();
                FindAnyObjectByType<ActionDVD>().FindItem();
                break;
            case Pile.BiographyMatt:
                item = FindAnyObjectByType<BiographyMatt>();
                FindAnyObjectByType<BiographyMatt>().FindItem();
                break;
            case Pile.PoemBook:
                item = FindAnyObjectByType<PoemBook>();
                FindAnyObjectByType<PoemBook>().FindItem();
                break;
            case Pile.ComedyDVD:
                item = FindAnyObjectByType<ComedyDVD>();
                FindAnyObjectByType<ComedyDVD>().FindItem();
                break;
            default:
                Debug.Log("Fail");
                break;
        }
    }
}
