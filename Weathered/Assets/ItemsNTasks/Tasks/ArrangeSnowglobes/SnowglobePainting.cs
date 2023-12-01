using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowglobePainting : Interaction
{
    public GameObject investigateObjectPrefab;
    [SerializeField] string paintingName;
    public override void onClick()
    {
        ObservationMenu.observeMenu.ClearVisualRoot();
        Instantiate(investigateObjectPrefab, ObservationMenu.observeMenu.visualRoot.transform);
        ObservationMenu.observeMenu.SetDescText("");
        ObservationMenu.observeMenu.nameText.text = paintingName;
        UIController.UIControl.OpenInteractionMenu();   
    }
}
