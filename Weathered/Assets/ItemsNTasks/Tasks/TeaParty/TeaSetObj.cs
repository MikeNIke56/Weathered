using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaSetObj : Interaction
{
    [SerializeField] TeaSet teaSet;

    public override void onClick()
    {
        if (teaSet == null)
        {
            teaSet = FindFirstObjectByType<TeaSet>();
        }
        teaSet.ClickedTeaSetObject(this);
    }
}
