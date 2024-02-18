using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseObj : Interaction
{
    [SerializeField] Fuse fuse;

    public override void onClick()
    {
        if (fuse == null)
        {
            fuse = FindFirstObjectByType<Fuse>();
        }
        fuse.ClickedFuseObject(this);
    }
}
