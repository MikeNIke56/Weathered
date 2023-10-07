using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    InteractionMenu interactionMenu;

    public static ItemController i { get; private set; }

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        interactionMenu = FindAnyObjectByType<InteractionMenu>(FindObjectsInactive.Include);
    }

    public void DisplayItem(Item item)
    {
        bool itemClicked = item.Display();

        if (itemClicked && item.isPartOfTask==false)
        {
            interactionMenu.item = item;
            interactionMenu.gameObject.SetActive(true);
            interactionMenu.DisplayInfo(item);
        }
    }
}
