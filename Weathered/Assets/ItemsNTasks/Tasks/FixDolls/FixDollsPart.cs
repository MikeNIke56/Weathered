using UnityEngine;

public class FixDollsPart : Interaction
{
    [SerializeField] FixDolls fixTask;
    enum PartItems { ClemmyShoe, SaintHat, SallyEye, BenniWing, MrBearArm }
    [SerializeField] PartItems partItem;
    [SerializeField] Item armRemoverItem;
    [SerializeField] GameObject raccoonObject;
    [SerializeField] GameObject raccoonReplacement;

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
                if (ItemController.itemInHand == armRemoverItem || armRemoverItem == null)
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

    public void CheckForArm()
    {
        if (raccoonObject != null && raccoonReplacement != null)
        {
            raccoonObject.SetActive(false);
            raccoonReplacement.SetActive(true);
        }
    }
}
