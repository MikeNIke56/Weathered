using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowglobe : Item
{
    public ArrangeSnowglobes snowglobes;
    public bool chosen = false;

    public GameObject inspectUI;
    public GameObject snowGlobesObj;

    public enum sgType { Starvation, Ambush, Murder, Punishment, Revenge, Drowning, Glory, Dehydration }
    public sgType currentSGType;

    SnowglobesButtons sgButtons;

    private void Start()
    {
        sgButtons = FindAnyObjectByType<SnowglobesButtons>(FindObjectsInactive.Include);
    }

    public void OnClickedSGObject(SnowglobeObj clickedSG)
    {
        if(snowglobes.currentSGState == ArrangeSnowglobes.SGState.OutOfShelf)
        {
            ItemController.AddItemToHand(this);
            clickedSG.gameObject.SetActive(false);
        }
        else if (snowglobes.currentSGState == ArrangeSnowglobes.SGState.InShelf)
        {           
            //show date of the snowglobe
            inspectUI.SetActive(true);
            snowGlobesObj.SetActive(false);
            sgButtons.clickedSG = this;
            snowglobes.currentSGState = ArrangeSnowglobes.SGState.InspectingSG;
        }
        else if (snowglobes.currentSGState == ArrangeSnowglobes.SGState.InspectingSG)
        {
            //handle switch or inspect snowglobe
            ItemController.AddItemToHand(this);
            snowGlobesObj.SetActive(true);
            inspectUI.SetActive(false);
            Debug.Log("chose this snowlobe");
        }
    }
}
