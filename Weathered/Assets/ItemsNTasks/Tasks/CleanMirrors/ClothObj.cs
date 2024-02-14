using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothObj : Interaction
{
    [SerializeField] MirrorCloth clothItem;

    public override void onClick()
    {
        if (clothItem == null)
        {
            clothItem = FindFirstObjectByType<MirrorCloth>();
        }
        clothItem.ClickedClothObject(this);
    }
}
