using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaTable : Interaction
{
    [SerializeField] TeaParty teaParty;

    public override void onClick()
    {
        if (teaParty == null)
        {
            teaParty = FindFirstObjectByType<TeaParty>();
        }
        teaParty.ClickedTable(this);
    }
}
