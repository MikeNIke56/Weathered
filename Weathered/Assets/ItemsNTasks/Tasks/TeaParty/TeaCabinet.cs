using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaCabinet : Interaction
{
    [SerializeField] TeaSet teaSet;
    public bool hasGiven = false;

    public override void onClick()
    {
        if (teaSet == null)
        {
            teaSet = FindFirstObjectByType<TeaSet>();
        }

        if(hasGiven==false)
        {
            teaSet.ClickedTeaSetObject(teaSet.teaSetObject);
            hasGiven = true;
            ShortTextController.STControl.AddShortText("This looks valuable, better be careful...");
        }     
    }
}
