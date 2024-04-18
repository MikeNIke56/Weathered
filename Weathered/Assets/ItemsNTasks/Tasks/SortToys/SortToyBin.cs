using System.Collections.Generic;
using UnityEngine;

public class SortToyBin : Interaction
{
    SortToys sortTask;
    [SerializeField] List<Item> validToys;

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
