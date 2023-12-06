using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SnowglobePainting : Interaction
{
    public GameObject investigateObjectPrefab;
    [SerializeField] string paintingName;
    [SerializeField] Sprite inspectImg;
    [SerializeField] AudioSource bookOpen;
    public override void onClick()
    {
        ObservationMenu.observeMenu.ClearVisualRoot();
        var spawnObj = Instantiate(investigateObjectPrefab, ObservationMenu.observeMenu.visualRoot.transform);
        ObservationMenu.observeMenu.SetDescText("");
        ObservationMenu.observeMenu.nameText.text = paintingName;
        spawnObj.GetComponent<Image>().sprite = inspectImg;

        if(paintingName == "Till Death Do Part" || paintingName == "There is no Peace Like Death")
            bookOpen.Play();

        UIController.UIControl.OpenInteractionMenu();   
    }
}
