using UnityEngine;

public class HandItemInteraction : MonoBehaviour
{
    public void ClickedHand()
    {
        if (ItemController.itemInHand != null)
        {
            ItemController.itemInHand.InvestigateItem();
        }
    }
}
