using UnityEngine;

public class ItemController : MonoBehaviour
{
    static public ItemController itemControl;
    static public Item itemInHand;
    public GameObject HandIconRoot;
    public AudioSource itemPickupAudio;

    public Item tempItem;

    void Start()
    {
        itemControl = this;
    }
    private void Update()
    {
        tempItem = itemInHand;
    }
    public static bool AddItemToHand(Item itemToAdd)
    {
        /*if (itemInHand == itemToAdd)
        {
            return false;
        }*/

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

    public static void DropItemInHand()
    {
        if (itemInHand != null)
        {
            itemInHand.OnDropped();
        }

        itemInHand = null;
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
