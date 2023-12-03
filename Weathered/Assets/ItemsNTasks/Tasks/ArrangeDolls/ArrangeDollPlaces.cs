using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeDollPlaces : Interaction
{
    [SerializeField] ArrangeDolls ADTask;
    public Item correctDoll;
    public Item currentDoll;
    public bool isCorrect;
    public Transform dollSitPosition;
    public bool isInFront = false;
    public int facingNum = 0; //num < 0 is left facing chair, 0 is center, num > 0 is right facing chair
    public SittingDoll satDoll;

    public override void onClick()
    {
        if (ADTask == null)
        {
            ADTask = FindFirstObjectByType<ArrangeDolls>();
        }

        ADTask.PlaceClicked(this);
    }

    public void RetrieveDoll()
    {
        if (currentDoll == null)
        {
            return;
        }
        ItemController.AddItemToHand(currentDoll);
        foreach (Transform childTransform in dollSitPosition)
        {
            Destroy(childTransform.gameObject);
        }
        isCorrect = false;
        currentDoll = null;
        satDoll = null;
    }
    public void SetDoll(Item dollInHand)
    {
        ItemController.ClearItemInHand();
        if (currentDoll != null)
        {
            ItemController.AddItemToHand(currentDoll);
            foreach (Transform childTransform in dollSitPosition)
            {
                Destroy(childTransform.gameObject);
            }
        }
        currentDoll = dollInHand;
        if (currentDoll == correctDoll)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
    }
}
