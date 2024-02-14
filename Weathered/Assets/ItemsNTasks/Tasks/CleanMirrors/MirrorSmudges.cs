using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSmudges : Interaction
{
    [SerializeField] CleanMirrors cleanMirrors;
    public int cleanCount;

    public override void onClick()
    {
        if (cleanMirrors == null)
        {
            cleanMirrors = FindFirstObjectByType<CleanMirrors>();
        }

        cleanMirrors.ClickedSmudge(this);
    }
}
