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
                    clickedBox.OpenBox();
                }
                else if (box1ItemList.Count > 0)
                {
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
                    clickedBox.RemoveBox();
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
                RemoveItemFromList(heldItem);
                ItemController.ClearItemInHand();
            }
            else if (sortClicked.sortCategory == SortBoxesOutput.sortCategories.Collectibles && ColSortItems.Contains(heldItem))
            {
                RemoveItemFromList(heldItem);
                ItemController.ClearItemInHand();
            }
            else if (sortClicked.sortCategory == SortBoxesOutput.sortCategories.Taxidermy && TaxSortItems.Contains(heldItem))
            {
                RemoveItemFromList(heldItem);
                ItemController.ClearItemInHand();
            }
        }
    }
}