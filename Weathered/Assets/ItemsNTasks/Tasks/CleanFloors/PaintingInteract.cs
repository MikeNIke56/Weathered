using UnityEngine;

public class PaintingInteract : Interaction
{
    [SerializeField] Item triggerItem;
    [SerializeField] CleanFloor cleanFloor;

    public override void onClick()
    {
        if (ItemController.itemInHand == triggerItem)
        {
            cleanFloor.OnFailed();
        }
        else
            ShortTextController.STControl.AddShortText("Interesting painting");
    }
}
