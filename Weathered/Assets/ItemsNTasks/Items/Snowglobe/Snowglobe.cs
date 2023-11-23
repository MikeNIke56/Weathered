using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snowglobe : Item
{
    public ArrangeSnowglobes snowglobes;
    public bool chosen = false;
    public bool included = false;

    public GameObject inspectUI;
    public GameObject snowGlobesObj;

    public enum sgType { Starvation, Ambush, Murder, Punishment, Revenge, Drowning, Glory, Dehydration }
    public sgType currentSGType;
    public Image sgImg;

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
            //handle switching or inspecting a snowglobe

            if(snowglobes.isSwitching == true)
            {
                foreach (Transform sgObj in snowGlobesObj.transform)
                {
                    if(sgObj.GetComponent<SnowglobeObj>() != null && sgObj.GetComponent<SnowglobeObj>() == sgButtons.chosenSGObj)
                    {
                        //sets the first choice's values to the second's values
                        sgObj.GetComponent<SnowglobeObj>().sgItem = clickedSG.sgItem;
                        sgObj.GetComponent<SnowglobeObj>().sgItem.sgImg.sprite = clickedSG.sgItem.sgImg.sprite;

                        //sets the second's choice's values to the temp's values
                        clickedSG.sgItem = snowglobes.tempSG;
                        clickedSG.sgItem.sgImg.sprite = snowglobes.tempImg.sprite;

                        snowglobes.isSwitching = false;
                        ItemController.ClearItemInHand();

                        UpdateSnowGlobes();
                    }
                }
            }
            else
            {
                inspectUI.SetActive(true);
                snowGlobesObj.SetActive(false);
                sgButtons.curSG = this;
                sgButtons.curSGObj = clickedSG;
                snowglobes.currentSGState = ArrangeSnowglobes.SGState.InspectingSG;
            }         
        }
        else if (snowglobes.currentSGState == ArrangeSnowglobes.SGState.InspectingSG)
        {
            //handle choose and inspect snowglobe
            ItemController.AddItemToHand(this);
            snowGlobesObj.SetActive(true);
            inspectUI.SetActive(false);
            Debug.Log("chose this snowlobe");
        }
    }

    private void UpdateSnowGlobes()
    {
        foreach (Transform obj in snowGlobesObj.transform)
        {
            var objImg = obj.gameObject.GetComponent<Image>();
            if(objImg.sprite != null )
                objImg.sprite = obj.gameObject.GetComponent<SnowglobeObj>().sgItem.sgImg.sprite;
            else
                objImg.sprite = null;
        }
    }
}
