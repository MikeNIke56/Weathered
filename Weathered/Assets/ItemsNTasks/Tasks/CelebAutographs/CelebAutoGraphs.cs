using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebAutoGraphs : Task
{
    [SerializeField] GameObject[] objects2Dis;
    public bool[] requirementsMet = { false, false, false };
    public bool[] requirementsMetFlowers = { false, false, false };
    public int completedDolls = 0;

    public override void InstanceTask()
    {
        base.InstanceTask();

        if (currentState == taskState.Available)
            currentState = taskState.InProgress;
    }
    

    public override void OnFailed()
    {
        //trigger death condition
        Debug.Log("player has died");
    }

    public void BloomRose(Plant plant)
    {
        plant.rose.SetActive(true);
    }

    public void CheckCompleted()
    {
        if (completedDolls >= 3)
            OnCompleted();
    }

    public void UpdatePlants(int num, Plant plant)
    {
        bool isDone = true;
        for (int i = 0; i < requirementsMet.Length; i++)
        {
            if(i == num)
                requirementsMetFlowers[i] = true;
            else
            {
                if(requirementsMetFlowers[i] == false)
                    isDone = false;
            }
        }
 
        if (isDone)
        {
            if (num < 0 || num > 2)
            {
                UpdatePlants(num, plant);
            }
            else
                BloomRose(plant);
        }

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
