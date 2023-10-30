using System.Collections;
using System.Collections.Generic;
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
