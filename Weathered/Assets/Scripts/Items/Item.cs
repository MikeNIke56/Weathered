using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Create new Item")]
public class Item : ItemBase
{
    [SerializeField] Item boxSortItem;

    public Item BoxSortItem => boxSortItem;
    public override bool Display() { return true; }
}
