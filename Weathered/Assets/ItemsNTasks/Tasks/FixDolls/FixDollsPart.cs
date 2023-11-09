using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDollsPart : Interaction
{
    [SerializeField] FixDolls fixTask;
    enum PartItems { ClemmyShoe, SaintHat, SallyEye, BenniWing, MrBearArm}
    [SerializeField] PartItems partItem;
    [SerializeField] Item armRemoverItem;

    public override void onClick()
    {
        if (fixTask == null)
        {
            fixTask = FindFirstObjectByType<FixDolls>();
        }

        fixTask.ClickedPart(this);
    }
    public Item GetPartItem()
    {
        switch (partItem)
        {
            case PartItems.ClemmyShoe:
                return FindFirstObjectByType<ClemmyShoe>();
            case PartItems.SaintHat:
                return FindFirstObjectByType<SaintHat>();
            case PartItems.SallyEye:
                return FindFirstObjectByType<SallyEye>();
            case PartItems.BenniWing:
                return FindFirstObjectByType<BenniWing>();
            default:
                if (ItemController.itemInHand == armRemoverItem)
                {
                    //RemovalSFX
                    return FindFirstObjectByType<MrBearArm>();
                }
                else
                {
                    return null;
                }
        }
    }
}
