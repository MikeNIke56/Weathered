using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    static public ItemController itemControl;
    static public Item itemInHand;
    public GameObject HandIconRoot;

    void Start()
    {
        itemControl = FindFirstObjectByType<ItemController>();
    }
    public static bool AddItemToHand(Item itemToAdd)
    {
        if (itemInHand == itemToAdd)
        {
            return false;
        }

        try
        {
            itemToAdd.CheckIfHoldable();
        }
        catch
        {
            Debug.Log("Cannot check if item is holdable!");
            return false;
        }

        if (itemToAdd.CheckIfHoldable())
        {
            if (itemInHand != null)
            {
                itemInHand.OnReplaced();
            }
            itemInHand = itemToAdd;
            itemInHand.OnHeld();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ClearItemInHand()
    {
        if (itemInHand != null)
        {
            itemInHand.ClearItem();
        }

        itemInHand = null;
    }
}
