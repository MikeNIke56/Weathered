using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowglobeObj : Interaction
{
    public Snowglobe sgItem;
    public override void onClick()
    {
        if (sgItem == null)
        {
            Snowglobe[] allSgs = FindObjectsByType<Snowglobe>(FindObjectsSortMode.None);
            foreach(Snowglobe sg in allSgs)
            {
                if(sg.currentSGType == Snowglobe.sgType.Placeholder)
                {
                    sgItem = sg;
                    break;
                }
            }    
            sgItem.OnClickedSGObject(this);
        }
        else
            sgItem.OnClickedSGObject(this);
    }
}
