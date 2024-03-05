using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaCabinet : Interaction
{
    [SerializeField] TeaSet teaSet;
    [SerializeField] GameObject CabinetSetLayer;
    [SerializeField] GameObject CabinetEmptyLayer;
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
            ShortTextController.STControl.AddShortText("This looks valuable, better be careful...");
            LoadCabinet();
        }     
    }

    public void LoadCabinet()
    {
        hasGiven = true;
        CabinetEmptyLayer.SetActive(true);
        CabinetSetLayer.SetActive(false);
    }
}
