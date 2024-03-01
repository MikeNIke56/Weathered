using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvObj : Interaction
{
    DVDRitual ritual;

    private void Start()
    {
        ritual = FindAnyObjectByType<DVDRitual>();
    }
    public override void onClick()
    {
        ritual.ClickedTV();
    }
}
