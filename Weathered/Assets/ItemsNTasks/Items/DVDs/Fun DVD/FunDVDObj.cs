using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunDVDObj : Interaction
{
    [SerializeField] FunDVD funDVD;

    public override void onClick()
    {
        if (funDVD == null)
        {
            funDVD = FindFirstObjectByType<FunDVD>();
        }
        funDVD.ClickedDVDObject(this);
    }
}
