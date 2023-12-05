using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snowglobe : Item
{
    public ArrangeSnowglobes snowglobes;
    public bool chosen = false;
    public bool included = false;
    public bool isShowing;
    public int year;

    public GameObject inspectUI;
    public GameObject snowGlobesObj;

    public enum sgType { Starvation, Ambush, Murder, Punishment, Revenge, Drowning, Glory, Dehydration, Placeholder }
    public sgType currentSGType;
    public Image sgImg;
    public Image underSgImg;

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
                    if(sgObj.GetComponent<SnowglobeObj>().sgItem != null && sgObj.GetComponent<SnowglobeObj>() == sgButtons.chosenSGObj)
                    {
                        //sets the first choice's values to the second's values
                        sgObj.GetComponent<SnowglobeObj>().sgItem = clickedSG.sgItem;
                        sgObj.GetComponent<SnowglobeObj>().sgItem.sgImg.sprite = clickedSG.sgItem.sgImg.sprite;

                        //sets the second's choice's values to the temp's values
                        if (clickedSG.sgItem.chosen == true)
                        {
                            clickedSG.sgItem = snowglobes.tempSG;
                            clickedSG.sgItem.sgImg.sprite = snowglobes.tempImg.sprite;
                            snowglobes.isSwitching = false;
                            ItemController.ClearItemInHand();

                            UpdateSnowGlobes();
                            snowglobes.UpdateShelf();
                            snowglobes.CheckForFinished();
                        }
                    }
                }
            }
            else
            {
                try
                {
                    if (clickedSG.sgItem != null && clickedSG.sgItem.included == true)
                    {
                        sgButtons.curSG = this;
                        sgButtons.curSGObj = clickedSG;
                        snowglobes.currentSGState = ArrangeSnowglobes.SGState.InspectingSG;
                    }
                    else if (clickedSG.sgItem.currentSGType == sgType.Placeholder)
                    {
                        Snowglobe snowglobeInHand = (Snowglobe)ItemController.itemInHand;

                        for (int i = 0; i < snowglobes.snowGlobes.Count; i++)
                        {
                            if (snowglobeInHand.currentSGType == sgType.Dehydration && snowglobes.snowGlobes[i].currentSGType == sgType.Dehydration)
                            {
                                clickedSG.sgItem = snowglobes.snowGlobes[i];
                                clickedSG.sgItem.sgImg.sprite = snowglobes.snowGlobes[i].sgImg.sprite;
                                clickedSG.sgItem.included = true;
                                var objImg = clickedSG.gameObject.GetComponent<Image>();
                                objImg.sprite = objImg.gameObject.GetComponent<SnowglobeObj>().sgItem.sgImg.sprite;
                                clickedSG.gameObject.GetComponent<Image>().color = Color.white;

                                ItemController.ClearItemInHand();
                                ItemController.itemInHand = null;
                                snowglobes.UpdateShelf();
                            }
                            else if (snowglobeInHand.currentSGType == sgType.Glory && snowglobes.snowGlobes[i].currentSGType == sgType.Glory)
                            {
                                clickedSG.sgItem = snowglobes.snowGlobes[i];
                                clickedSG.sgItem.sgImg.sprite = snowglobes.snowGlobes[i].sgImg.sprite;
                                clickedSG.sgItem.included = true;
                                var objImg = clickedSG.gameObject.GetComponent<Image>();
                                objImg.sprite = objImg.gameObject.GetComponent<SnowglobeObj>().sgItem.sgImg.sprite;
                                clickedSG.gameObject.GetComponent<Image>().color = Color.white;

                                ItemController.ClearItemInHand();
                                ItemController.itemInHand = null;
                                snowglobes.UpdateShelf();
                            }
                        } 
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("Snowglobe doesn't exist");
                    Debug.Log(e);
                }

            }         
        }
        if (snowglobes.currentSGState == ArrangeSnowglobes.SGState.InspectingSG)
        {
            //handle choose and inspect snowglobe
            inspectUI.SetActive(true);
            snowGlobesObj.SetActive(false);
            snowglobes.snowGlobesObjUnder.GetComponent<Image>().sprite = clickedSG.sgItem.underSgImg.sprite;
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
