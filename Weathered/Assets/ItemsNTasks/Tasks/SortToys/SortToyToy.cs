using UnityEngine;

public class SortToyToy : Interaction
{

    SortToys sortTask;
    [SerializeField] GameObject origObject;
    [SerializeField] Item linkedToy;

    public override void onClick()
    {
        if (sortTask == null)
        {
            sortTask = FindFirstObjectByType<SortToys>();
        }

        sortTask.ClickToy(this);
    }

    public void PickUpToy()
    {
        ItemController.AddItemToHand(linkedToy);
        origObject.SetActive(false);
    }

    public void ResetThisToy(Item failedToy)
    {
        if (failedToy == linkedToy)
        {
            origObject.SetActive(true);
        }
    }
}
