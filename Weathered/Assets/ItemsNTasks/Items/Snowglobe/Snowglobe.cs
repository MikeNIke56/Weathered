using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowglobe : Item
{
    [SerializeField] SnowglobeObj sgObject;
    [SerializeField] ArrangeSnowglobes snowglobes;
    public bool chosen = false;

    public enum sgType { Starvation, Ambush, Murder, Punishment, Revenge, Drowning, Glory, Dehydration }
    public sgType currentSGType;
    public void OnClickedSGObject(SnowglobeObj clickedSG)
    {
        if(snowglobes.currentSGState == ArrangeSnowglobes.SGState.OutOfShelf)
        {
            ItemController.AddItemToHand(this);
            clickedSG.gameObject.SetActive(false);
        }
        else if (snowglobes.currentSGState == ArrangeSnowglobes.SGState.InShelf)
        {
            //handle switch or inspect snowglobe
        }
        else if (snowglobes.currentSGState == ArrangeSnowglobes.SGState.InspectingSG)
        {
            //show date of the snowglobe
        }
    }
}
