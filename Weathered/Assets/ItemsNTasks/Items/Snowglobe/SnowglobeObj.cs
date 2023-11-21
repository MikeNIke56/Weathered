using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowglobeObj : Interaction
{
    public Snowglobe sgItem;
    public override void onClick()
    {
        if (sgItem == null)
        {
            sgItem = FindFirstObjectByType<Snowglobe>();
        }

        sgItem.OnClickedSGObject(this);
    }
}
