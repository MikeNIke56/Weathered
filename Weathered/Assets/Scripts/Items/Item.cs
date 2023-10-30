using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interaction
{
    public string itemName;
    public string description;
    public string shortName;
    public string shortDescription;
    public enum itemState { None, Held, Dropped, OnFloor }
    public itemState currentState;
       // Prefabs for specific item conditions. Seperated as gameobjects to allow scripting and animation.
    public GameObject defaultObjectPrefab; // Default fallback. Always fill. 
    public GameObject itemUIObjectPrefab; // UI 'image' on screen while held.
    public GameObject playerObjectPrefab; // Mazarine holds this on her player model.
    public GameObject droppedObjectPrefab; // Item dropped in-world onto ground. Possibly has falling and settled state.
    public GameObject investigateObjectPrefab; // What appears in the investigation menu.
    public bool canInvestigate = false;
    GameObject currentUIObject;
    GameObject currentPlayerObject;
    GameObject currentDroppedObject;
    public virtual bool CheckIfHoldable()
    {
        return true;
    }
    public virtual void OnHeld()
    {
        
        if (itemUIObjectPrefab == null)
        {
            itemUIObjectPrefab = defaultObjectPrefab;
        }
        currentUIObject = Instantiate(itemUIObjectPrefab, ItemController.itemControl.HandIconRoot.transform);
        currentState = itemState.Held;
        ItemController.itemControl.itemPickupAudio.Play();
    }
    public virtual void OnReplaced()
    {
        OnDropped();
    }
    public virtual void OnDropped() 
    {
        Destroy(currentUIObject);
        currentState = itemState.Dropped;
    }
    public virtual void OnHitFloor() { }

    public virtual void ClearItem()
    {
        Destroy(currentUIObject);
        currentState = itemState.None;
    }

    public virtual void InvestigateItem()
    {
        if (canInvestigate)
        {
            if (investigateObjectPrefab == null)
            {
                investigateObjectPrefab = defaultObjectPrefab;
            }
            ObservationMenu.observeMenu.ClearVisualRoot();
            Instantiate(investigateObjectPrefab, ObservationMenu.observeMenu.visualRoot.transform);
            ObservationMenu.observeMenu.SetDescText(description);
            ObservationMenu.observeMenu.nameText.text = itemName;
            UIController.UIControl.OpenInteractionMenu();
        }
    }
}
