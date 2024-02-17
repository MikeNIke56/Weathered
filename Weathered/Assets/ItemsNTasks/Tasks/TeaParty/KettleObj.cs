using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KettleObj : Interaction
{
    [SerializeField] Kettle kettle;

    public override void onClick()
    {
        if (kettle == null)
        {
            kettle = FindFirstObjectByType<Kettle>();
        }
        kettle.ClickedKettleObject(this);
    }
}
