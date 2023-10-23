using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEntrance : Item
{
    [SerializeField] KeyEntranceObject key1Object;
    public void OnClickedKeyObject(KeyEntranceObject clickedKey)
    {
        ItemController.AddItemToHand(this);
        clickedKey.gameObject.SetActive(false);
    }
    public override void OnReplaced()
    {
        base.OnReplaced();
        key1Object.gameObject.SetActive(true);
    }
}
