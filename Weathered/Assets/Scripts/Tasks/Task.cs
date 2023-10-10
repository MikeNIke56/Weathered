using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Task/Create new Task")]
public class Task : TaskBase
{
    public void StartTask()
    {
        Debug.Log("task started");

        switch (Name)
        {
            case "Sort Boxes":
                Debug.Log("Working");
                break;
            case "Task":
                //TaskController.i.BeginTask((Task)obj.collider.gameObject.GetComponent<TaskDetermine>().ChosenTask);
                break;
            case "Item":
                //ItemController.i.HandleItem((Item)obj.collider.gameObject.GetComponent<ItemDetermine>().ChosenItem);
                break;
            default:
                Debug.Log("test");
                break;
        }
    }
}
