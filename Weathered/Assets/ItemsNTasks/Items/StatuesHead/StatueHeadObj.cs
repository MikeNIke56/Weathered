using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHeadObj : Interaction
{
    [SerializeField] StatueHead statueHead;

    public override void onClick()
    {
        if (statueHead == null)
        {
            statueHead = FindFirstObjectByType<StatueHead>();
        }
        statueHead.ClickedStatueObject(this);
    }
}
