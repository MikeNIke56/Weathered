using UnityEngine;

public class SortBoxesOutput : Interaction
{
    [SerializeField] SortBoxes SBTask;
    public enum sortCategories { ChildrensToys, Collectibles, Taxidermy }
    public sortCategories sortCategory;

    public override void onClick()
    {
        if (SBTask == null)
        {
            SBTask = FindFirstObjectByType<SortBoxes>();
        }

        SBTask.SortClicked(this);
    }
}
