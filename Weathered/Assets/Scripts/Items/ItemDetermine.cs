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

    ItemBase ChooseItem()
    {
        chosenItem = items[Random.Range(0, items.Length)];
        gameObject.GetComponent<SpriteRenderer>().sprite = chosenItem.OverWorldIcon;
        return chosenItem;
    }

    public ItemBase ChosenItem => chosenItem;
}
