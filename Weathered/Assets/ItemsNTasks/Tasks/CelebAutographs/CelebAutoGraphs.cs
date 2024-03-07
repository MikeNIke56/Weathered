using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebAutoGraphs : Task
{
    [SerializeField] GameObject[] objects2Dis;

    public override void InstanceTask()
    {
        base.InstanceTask();
        
    }
    

    public override void OnFailed()
    {
        //trigger death condition
        Debug.Log("player has died");
    }

    void DisableObjects()
    {
        for (int i = 0; i < objects2Dis.Length; i++)
        {
            if (objects2Dis[i].tag == "Untagged")
                objects2Dis[i].SetActive(false);
            else if (objects2Dis[i].tag == "Interactable")
                objects2Dis[i].tag = "Untagged";
        }
    }

    public override void LoadFinishedTask()
    {
       
        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
