using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopObject : Interaction
{
    [SerializeField] Mop mopItem;
    public bool pickedUpMop = false;

    public override void onClick()
    {
        if (mopItem == null)
        {
            mopItem = FindFirstObjectByType<Mop>();
        }
        mopItem.ClickedMopObject(this);

        pickedUpMop = true;
    }
}
