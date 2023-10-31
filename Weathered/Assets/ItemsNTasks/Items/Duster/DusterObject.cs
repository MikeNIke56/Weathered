using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusterObject : Interaction
{
    [SerializeField] Duster dusterItem;
    public bool pickedUpBroom = false;

    public override void onClick()
    {
        if (dusterItem == null)
        {
            dusterItem = FindFirstObjectByType<Duster>();
        }
        dusterItem.ClickedDusterObject(this);

        pickedUpBroom = true;

        if(TutorialDialog.i.boxesFirst == false)
            TutorialDialog.i.cobWebsFirst = true;
    }
}
