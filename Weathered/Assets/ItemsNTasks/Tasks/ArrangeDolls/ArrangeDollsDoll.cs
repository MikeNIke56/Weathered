using UnityEngine;

public class ArrangeDollsDoll : Interaction
{
    [SerializeField] ArrangeDolls ADTask;
    enum DollItems { Clemmy, SallyMae, MrBear, SaintBearnard, Benni }
    [SerializeField] DollItems dollItem;
    public override void onClick()
    {
        if (ADTask == null)
        {
            ADTask = FindFirstObjectByType<ArrangeDolls>();
        }

        ADTask.DollClicked(this);
    }

    public Item GetDollItem()
    {
        switch (dollItem)
        {
            case DollItems.Clemmy:
                return FindFirstObjectByType<Clemmy>();
            case DollItems.SallyMae:
                return FindFirstObjectByType<SallyMae>();
            case DollItems.MrBear:
                return FindFirstObjectByType<MrBear>();
            case DollItems.SaintBearnard:
                return FindFirstObjectByType<SaintBearnard>();
            default:
                return FindFirstObjectByType<Benni>();
        }
    }
}
