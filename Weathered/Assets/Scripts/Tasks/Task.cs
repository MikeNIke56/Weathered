using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Task/Create new Task")]
public class Task : TaskBase
{
    public void StartTask()
    {
        Debug.Log("task started");
    }
}
