using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeabagsCabinet : Interaction
{
    [SerializeField] Teabags teaBags;
    public bool canStillInteract = true;

    public override void onClick()
    {
        if (teaBags == null)
        {
            teaBags = FindFirstObjectByType<Teabags>();
        }

        if(canStillInteract==true)
            teaBags.ClickedTeaBagObject(teaBags.teaBagsObject);
        else
            ShortTextController.STControl.AddShortText("I don’t need more tea...");
    }
}
