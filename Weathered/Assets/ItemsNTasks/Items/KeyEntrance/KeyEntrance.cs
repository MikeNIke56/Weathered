using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEntrance : Item
{
    [SerializeField] KeyEntranceObject key1Object;
    [SerializeField] Task taskToComplete;
    public void OnClickedKeyObject(KeyEntranceObject clickedKey)
    {
        if (taskToComplete == null || (taskToComplete != null && taskToComplete.currentState == Task.taskState.Completed))
        {
            ItemController.AddItemToHand(this);
            clickedKey.gameObject.SetActive(false);
        }
    }
    public override void OnReplaced()
    {
        base.OnReplaced();
        key1Object.gameObject.SetActive(true);
    }
}
