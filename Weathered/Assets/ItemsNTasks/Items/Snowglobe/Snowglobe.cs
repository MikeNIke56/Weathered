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
                Snowglobe tempSnowglobe = new Snowglobe();
                Image tempImg = null;

                //sets the temp's values to the first choice's values
                tempImg = sgButtons.chosenSGObj.sgItem.sgImg;
                tempImg.sprite = sgButtons.chosenSGObj.sgItem.sgImg.sprite;
                tempSnowglobe = sgButtons.chosenSGObj.sgItem;

                //sets the first choice's values to the second's values
                foreach (Transform sgObj in snowGlobesObj.transform)
                {
                    if(sgObj.GetComponent<SnowglobeObj>() != null && sgObj.GetComponent<SnowglobeObj>() == sgButtons.chosenSGObj)
                    {
                        var objImg1 = sgButtons.chosenSGObj.sgItem.sgImg;
                        objImg1.sprite = clickedSG.sgItem.sgImg.sprite;
                        sgButtons.chosenSGObj.sgItem = clickedSG.sgItem;

                        //sets the second's choice's values to the first's values
                        var objImg2 = clickedSG.sgItem.sgImg;
                        objImg2.sprite = tempImg.sprite;
                        clickedSG.sgItem = tempSnowglobe;

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
            var objImg = obj.GetComponent<Image>();
            objImg.sprite = obj.GetComponent<SnowglobeObj>().sgItem.sgImg.sprite;
        }
    }
}
