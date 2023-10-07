using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetermine : MonoBehaviour
{
    [SerializeField] ItemBase[] items;
    [SerializeField] ItemBase chosenItem;

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


    public ItemBase ChosenItem => chosenItem;
}
