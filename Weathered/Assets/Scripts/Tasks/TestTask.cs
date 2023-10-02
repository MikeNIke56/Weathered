using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Task/Create new Task")]
public class TestTask : TaskBase
{
    public override void StartTask()
    {
        isActive = true;
        Debug.Log("task started");
    }
}
