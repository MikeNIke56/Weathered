using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetKey : Item
{
    [SerializeField] ClosetKeyObject keyObject;
    [SerializeField] Task taskToComplete;
    public void OnClickedKeyObject(ClosetKeyObject clickedKey)
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
        keyObject.gameObject.SetActive(true);
    }
}
