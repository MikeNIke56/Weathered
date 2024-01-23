using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowglobesButtons : MonoBehaviour
{
    public GameObject inspectUI;
    public GameObject snowGlobesObj;
    public GameObject chooseButton;
    ArrangeSnowglobes arrangeSnowglobes;

    public Snowglobe curSG;
    public Snowglobe chosenSG;

    public SnowglobeObj curSGObj;
    public SnowglobeObj chosenSGObj;

    private void Start()
    {
        arrangeSnowglobes = FindAnyObjectByType<ArrangeSnowglobes>();
    }

    public void BackToSGs()
    {
        snowGlobesObj.SetActive(true);
        inspectUI.SetActive(false);
        arrangeSnowglobes.currentSGState = ArrangeSnowglobes.SGState.InShelf;
    }

    public void ChooseSnowGlobe()
    {
        if (curSG.included == true)
        {
            snowGlobesObj.SetActive(true);
            inspectUI.SetActive(false);
            chosenSG = curSG;
            chosenSGObj = curSGObj;
            arrangeSnowglobes.tempSG = chosenSG;
            arrangeSnowglobes.tempImg = chosenSGObj.sgItem.sgImg;
            ItemController.AddItemToHand(chosenSG);
            arrangeSnowglobes.currentSGState = ArrangeSnowglobes.SGState.InShelf;
            arrangeSnowglobes.isSwitching = true;
            chosenSG.pickup.Play();
        }
    }

    public void DisableButton()
    {
        chooseButton.GetComponent<Image>().color = Color.gray;
        chooseButton.GetComponent<Button>().enabled = false;
    }
}
