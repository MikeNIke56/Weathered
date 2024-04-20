using System;
using System.Collections.Generic;
using UnityEngine;

public class SortToyBin : Interaction
{
    SortToys sortTask;
    [SerializeField] List<Item> validToys;

    public enum BinType { Blocks, Stuffed, Game }; //states that the player can be in
    public BinType type;

    public void SearchToys()
    {   
        validToys.Clear();
        foreach (var toy in GameManager.GM.GetToyItems())
        {
            string typeName = toy.name;
            if (typeName.Contains(type.ToString()))
                validToys.Add(toy);
        }
    }

    public override void onClick()
    {
        if (sortTask == null)
        {
            sortTask = FindFirstObjectByType<SortToys>();
        }

        sortTask.ClickBin(this);
    }

    public bool CheckToy(Item toyCheck)
    {
        if (validToys.Contains(toyCheck))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
