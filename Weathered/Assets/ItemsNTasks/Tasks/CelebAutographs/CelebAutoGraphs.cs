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

    [SerializeField] FanDollsDialog[] dolls;
    ArvinLogic arvinLogic;
    Statue statue;
    Plant plant;
    ArvinAutograph autograph;

    [SerializeField] DoorScript auntsDoor;

    private void Start()
    {
        arvinLogic = FindAnyObjectByType<ArvinLogic>();
        statue = FindAnyObjectByType<Statue>();
        plant = FindAnyObjectByType<Plant>();
        autograph = FindAnyObjectByType<ArvinAutograph>();
    }

    public override void InstanceTask()
    {
        base.InstanceTask();

        if (currentState == taskState.Available)
            currentState = taskState.InProgress;
    }
    
    public void ClickedStatueObject(Statue statue)
    {
        if(ItemController.itemInHand is StatueHead)
        {
            statue.CompleteStatue();
        }
    }
    public void ClickedEagleObject(StuffedEagleOVObj stuffedEagle)
    {
        if (ItemController.itemInHand is FakeRock)
        {
            StartCoroutine(stuffedEagle.DropHead());
        }
        else
        {
            ShortTextController.STControl.AddShortText("That�s really high!");
        }
    }


    public void BloomRose(Plant plant)
    {
        plant.rose.SetActive(true);
    }

    public void CheckCompleted()
    {
        if (completedDolls >= 3)
        {
            auntsDoor.OpenDoor(true);
            OnCompleted();
        }
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
            if (objects2Dis[i].tag == "Interactable")
                objects2Dis[i].tag = "Untagged";
        }
    }

    public override void OnFailed()
    {
        //trigger death condition
        Debug.Log("player has died");
    }

    public override void LoadFinishedTask()
    {
        for (int i = 0; i < dolls.Length; i++)
        {
            dolls[i].Complete();
        }


        DisableObjects();
        requirementsMet[0] = true;
        requirementsMet[1] = true;
        requirementsMet[2] = true;
        arvinLogic.stage = 4;
        arvinLogic.autographsNotUsed = 0;
        arvinLogic.isSaved = true;
        statue.CompleteStatue();
        BloomRose(plant);
        autograph.SaveAutoGraph();
        auntsDoor.OpenDoor(true);

        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
