using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    InteractionMenu interactionMenu;
    PlayerController player;

    public static ItemController i { get; private set; }

    private void Awake()
    {
        i = this;
        player = FindAnyObjectByType<PlayerController>();
    }

    private void Start()
    {
        interactionMenu = FindAnyObjectByType<InteractionMenu>(FindObjectsInactive.Include);
    }

    public void HandleItem(Item item)
    {
        bool itemClicked = item.Display();

        if (itemClicked && item.isPartOfTask == false)
        {
            interactionMenu.item = item;
            interactionMenu.gameObject.SetActive(true);
            interactionMenu.DisplayInfo(item);
        }
        else if(itemClicked && item.isPartOfTask == true)
        {
            if (player.curItem == item.BoxSortItem)
                Debug.Log("correct");
        }
    }
}
