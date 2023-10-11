using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetermine : MonoBehaviour
{
    [SerializeField] ItemBase[] items;
    [SerializeField] ItemBase chosenItem;

    Item boxSortItem;

    private void Awake()
    {
        ChooseItem();
    }

    Item ChooseItem()
    {
        chosenItem = items[Random.Range(0, items.Length)];
        gameObject.GetComponent<SpriteRenderer>().sprite = chosenItem.OverWorldIcon;
        return (Item)chosenItem;
    }

    public void SetItem(Item item)
    {
        chosenItem = item;
        gameObject.GetComponent<SpriteRenderer>().sprite = chosenItem.OverWorldIcon;
    }
    public void SetBoxReactItem(Item[] items)
    {
        boxSortItem = items[Random.Range(0, items.Length)];

    }



    public ItemBase ChosenItem => chosenItem;
    public Item BoxSortItem => boxSortItem;
}
