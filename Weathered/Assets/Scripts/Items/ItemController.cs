using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

    public void DisplayItem(TaskBase item)
    {
        bool itemClicked = item.Display();

        if (itemClicked)
        {
            interactionMenu.gameObject.SetActive(true);
            interactionMenu.DisplayInfo(item);
        }
    }
}
