using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowglobePainting : Interaction
{
    public GameObject investigateObjectPrefab;
    public override void onClick()
    {
        ObservationMenu.observeMenu.ClearVisualRoot();
        Instantiate(investigateObjectPrefab, ObservationMenu.observeMenu.visualRoot.transform);
        UIController.UIControl.OpenInteractionMenu(); 
    }
}
