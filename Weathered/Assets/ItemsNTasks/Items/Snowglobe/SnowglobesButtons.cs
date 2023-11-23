using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowglobesButtons : MonoBehaviour
{
    public GameObject inspectUI;
    public GameObject snowGlobesObj;
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
            ItemController.AddItemToHand(chosenSG);
            arrangeSnowglobes.currentSGState = ArrangeSnowglobes.SGState.InShelf;
            arrangeSnowglobes.isSwitching = true;
            Debug.Log("chosen");
        }
    }
}
