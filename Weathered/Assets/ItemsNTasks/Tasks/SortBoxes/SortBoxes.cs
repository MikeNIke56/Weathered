using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortBoxes : Task
{
    [SerializeField] List<Item> possibleItemsInBoxes = new List<Item>();
    public List<Item> box1ItemList = new List<Item>();
    public List<Item> box2ItemList = new List<Item>();
    public List<Item> box3ItemList = new List<Item>();
    [SerializeField] SortBoxesBox Box1st, Box2nd, Box3rd;
    [SerializeField] List<Item> ToySortItems = new List<Item>();
    [SerializeField] List<Item> ColSortItems = new List<Item>();
    [SerializeField] List<Item> TaxSortItems = new List<Item>();

    [SerializeField] AccessStairs stairs; //refernce to stairs object in scene

    public bool startBox = false;
    public bool tookToy = false;
    public bool correctBox = false;
    public bool isDoneBox = false;
    public void BoxClicked(SortBoxesBox clickedBox)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
            if (possibleItemsInBoxes.Count == 9)
            {
                List<Item> tempItemList = new List<Item>(possibleItemsInBoxes);
                int tempRandNum = 0;
                while (tempItemList.Count > 0)
                {
                    tempRandNum = Random.Range(0, tempItemList.Count);
                    if (box1ItemList.Count < 3)
                    {
                        box1ItemList.Add(tempItemList[tempRandNum]);
                    }
                    else if (box2ItemList.Count < 3)
                    {
                        box2ItemList.Add(tempItemList[tempRandNum]);
                    }
                    else
                    {
                        box3ItemList.Add(tempItemList[tempRandNum]);
                    }
                    tempItemList.RemoveAt(tempRandNum);
                }
            }
        }

        if (currentState == taskState.InProgress)
        {
            if (clickedBox == Box1st)
            {
                if (!clickedBox.isOpened)
                {
                    startBox = true;

                    TutorialDialog.i.cobwebsArrow.SetActive(false);
                    TutorialDialog.i.boxesArrow.SetActive(false);

                    if (TutorialDialog.i.cobWebsFirst == false)
                        TutorialDialog.i.boxesFirst = true;

                    clickedBox.OpenBox();
                }
                else if (box1ItemList.Count > 0)
                {
                    if(tookToy == false)
                        tookToy = true;
                    ItemController.AddItemToHand(box1ItemList[Random.Range(0, box1ItemList.Count)]);
                }
                else if (!clickedBox.isDone)
                {
                    clickedBox.RemoveBox();
                }
            }
            else if (clickedBox == Box2nd && Box1st.isDone)
            {
                if (!clickedBox.isOpened)
                {
                    clickedBox.OpenBox();
                }
                else if (box2ItemList.Count > 0)
                {
                    ItemController.AddItemToHand(box2ItemList[Random.Range(0, box2ItemList.Count)]);
                }
                else if (!clickedBox.isDone)
                {
                    clickedBox.RemoveBox();
                }
            }
            else if (Box2nd.isDone)
            {
                if (!clickedBox.isOpened)
                {
                    clickedBox.OpenBox();
                }
                else if (box3ItemList.Count > 0)
                {
                    ItemController.AddItemToHand(box3ItemList[Random.Range(0, box3ItemList.Count)]);
                }
                else if (!clickedBox.isDone)
                {
                    isDoneBox = true;
                    clickedBox.RemoveBox();
                    stairs.isPassable = true;
                    OnCompleted();
                }
            }
        }
    }

    public void RemoveItemFromList(Item itemToRemove)
    {
        if (box1ItemList.Contains(itemToRemove))
        {
            box1ItemList.Remove(itemToRemove);
        }
        else if (box2ItemList.Contains(itemToRemove))
        {
            box2ItemList.Remove(itemToRemove);
        }
        else if (box3ItemList.Contains(itemToRemove))
        {
            box3ItemList.Remove(itemToRemove);
        }
    }

    public void SortClicked(SortBoxesOutput sortClicked)
    {
        Item heldItem = ItemController.itemInHand;
        if (heldItem != null && possibleItemsInBoxes.Contains(heldItem))
        {
            if (sortClicked.sortCategory == SortBoxesOutput.sortCategories.ChildrensToys && ToySortItems.Contains(heldItem))
            {
                if (correctBox == false)
                    correctBox = true;

                RemoveItemFromList(heldItem);
                ItemController.ClearItemInHand();
            }
            else if (sortClicked.sortCategory == SortBoxesOutput.sortCategories.Collectibles && ColSortItems.Contains(heldItem))
            {
                if (correctBox == false)
                    correctBox = true;

                RemoveItemFromList(heldItem);
                ItemController.ClearItemInHand();
            }
            else if (sortClicked.sortCategory == SortBoxesOutput.sortCategories.Taxidermy && TaxSortItems.Contains(heldItem))
            {
                if (correctBox == false)
                    correctBox = true;

                RemoveItemFromList(heldItem);
                ItemController.ClearItemInHand();
            }
            else
            {
                WrongItem();
                ItemController.ClearItemInHand();
            }
        }
    }

    void WrongItem()
    {
        OnBadAction();
        if (timesFailed == 1)
        {
            ShortTextController.STControl.AddShortText("No. That wasn't right...");
        } 
        else if (timesFailed == 2)
        {
            ShortTextController.STControl.AddShortText("I made another mistake...");
        } 
        else if (timesFailed >= 3)
        {
            ShortTextController.STControl.AddShortText("STOP. That's the wrong sorting box.", true);
        }
    }
}
