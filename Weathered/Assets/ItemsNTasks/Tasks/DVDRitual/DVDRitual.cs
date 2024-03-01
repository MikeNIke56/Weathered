using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVDRitual : Task
{
    public override void InstanceTask()
    {
        base.InstanceTask();
    }
    public void ClickedSummonCircle(Interaction interaction)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        if (interaction is BrokenGlass)
        {
            
        }
        else if (interaction is BloodSplatter)
        {
            
        }

    }
    public void ClickedTV()
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        

    }
    public override void OnFailed()
    {
        //trigger death condition
        Debug.Log("player has died");
    }

    public override void LoadFinishedTask()
    {      
        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
