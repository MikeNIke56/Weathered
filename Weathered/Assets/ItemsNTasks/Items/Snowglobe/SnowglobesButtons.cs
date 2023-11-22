using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowglobesButtons : MonoBehaviour
{
    public GameObject inspectUI;
    public GameObject snowGlobesObj;
    ArrangeSnowglobes arrangeSnowglobes;

    public Snowglobe clickedSG;
    Snowglobe chosenSG;

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
        snowGlobesObj.SetActive(true);
        inspectUI.SetActive(false);
        chosenSG = clickedSG;
        ItemController.AddItemToHand(chosenSG);
        arrangeSnowglobes.currentSGState = ArrangeSnowglobes.SGState.InShelf;
    }
}
