using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDolls : Task
{
    [SerializeField] List<Item> validParts;
    [SerializeField] List<Item> validDolls;
    int fixedDolls = 0;

    ArrangeDolls arrangeDolls;
    public GameObject[] dollPartsToDisable;

    private void Start()
    {
        arrangeDolls = FindAnyObjectByType<ArrangeDolls>(FindObjectsInactive.Include);
    }

    public void ClickedPart(FixDollsPart partClicked)
    {
        Item tempPart = partClicked.GetPartItem();
        if (tempPart == null)
        {
            return;
        }
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }
        ItemController.AddItemToHand(tempPart);
        partClicked.CheckForArm();
        if (partClicked)
        {
            Destroy(partClicked.gameObject);
        }
    }

    public void ClickedDoll(Item dollClicked, ArrangeDollPlaces placeClicked)
    {
        if (currentState == taskState.Completed)
        {

        }
        else
        {
            if (ItemController.itemInHand != null && validParts.Contains(ItemController.itemInHand))
            {
                if (ItemController.itemInHand == validParts[0] && dollClicked == validDolls[0])
                {
                    AttachPart(placeClicked);
                }
                else if (ItemController.itemInHand == validParts[1] && dollClicked == validDolls[1])
                {
                    AttachPart(placeClicked);
                }
                else if (ItemController.itemInHand == validParts[2] && dollClicked == validDolls[2])
                {
                    AttachPart(placeClicked);
                }
                else if (ItemController.itemInHand == validParts[3] && dollClicked == validDolls[3])
                {
                    AttachPart(placeClicked);
                }
                else if (ItemController.itemInHand == validParts[4] && dollClicked == validDolls[4])
                {
                    AttachPart(placeClicked);
                }
                else // Make specific fail conditions for [3]Saint, [4]Sally, and [2]Bear
                {
                    OnBadAction();
                    //FailCondition
                }
            }
        }
    }
    public void AttachPart(ArrangeDollPlaces placeClicked)
    {
        placeClicked.satDoll.isFixed = true;
        placeClicked.satDoll.PoseDoll(placeClicked.facingNum);
        fixedDolls++;
        if (fixedDolls >= 5)
        {
            OnCompleted();
            Progression.Prog.ToysDolls();
        }
        ItemController.ClearItemInHand();
    }

    public override void LoadFinishedTask()
    {
        foreach (var doll in arrangeDolls.allPlaces)
        {
            AttachPart(doll);
        }
        for (int i = 0; i < dollPartsToDisable.Length; i++)
        {
            dollPartsToDisable[i].SetActive(false);
        }

        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
