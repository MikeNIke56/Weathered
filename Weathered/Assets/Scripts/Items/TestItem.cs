using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Create new Item")]
public class TestItem : ItemBase
{
    public override bool Display()
    {
        return true;
    }
}
