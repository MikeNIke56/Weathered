using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDusterObject : Interaction
{
    [SerializeField] GlassDuster dusterItem;
    public bool pickedUpBroom = false;

    public override void onClick()
    {
        if (dusterItem == null)
        {
            dusterItem = FindFirstObjectByType<GlassDuster>();
        }
        dusterItem.ClickedDusterObject(this);

        pickedUpBroom = true;
    }
}
