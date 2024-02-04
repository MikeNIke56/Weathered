using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetInteract : Interaction
{
    [SerializeField] Item itemToOpen;
    [SerializeField] GameObject itemToGive;
    [SerializeField] GameObject itemToGive2;
    [SerializeField] Sprite doorOpen;
    [SerializeField] AudioSource doorOpenSfx;
    [SerializeField] AudioSource doorLockedSfx;

    bool isLocked = true;

    public override void onClick()
    {
        if (isLocked && ItemController.itemInHand == itemToOpen)
        {
            ItemController.ClearItemInHand();
            itemToGive.SetActive(true);
            itemToGive2.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
            doorOpenSfx.Play();
            gameObject.GetComponent<Interactable>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isLocked = false;
        }
        else if (isLocked && ItemController.itemInHand != itemToOpen)
        {
            ShortTextController.STControl.AddShortText("It's locked...");
            doorLockedSfx.Play();
        }
    }

    public void OnCompletedLoad()
    {
        itemToGive.SetActive(true);
        itemToGive2.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
        gameObject.GetComponent<Interactable>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        isLocked = false;
    }
}
