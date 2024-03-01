using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVDRitual : Task
{
    Item itemInHand;

    StrangeDVD strangeDVD;
    FunDVD funDVD;
    ThrillingDVD thrillingDVD;

    bool[] requirementsMet = {false, false, false};

    public override void InstanceTask()
    {
        base.InstanceTask();
        strangeDVD = FindAnyObjectByType<StrangeDVD>();
        funDVD = FindAnyObjectByType<FunDVD>();
        thrillingDVD = FindAnyObjectByType<ThrillingDVD>();
    }
    public void ClickedSummoningCircle(SummoningCircleObj summoningCircle)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        itemInHand = ItemController.itemInHand;
        switch (itemInHand)
        {
            case ActionDVD:
                Debug.Log("action dvd added");
                summoningCircle.candleLitCount++;
                summoningCircle.LightCandle();
                requirementsMet[0] = true;
                ItemController.ClearItemInHand();
                break;
            case ArvensHits:
                Debug.Log("arvens hits added");
                summoningCircle.candleLitCount++;
                summoningCircle.LightCandle();
                requirementsMet[1] = true;
                ItemController.ClearItemInHand();
                break;
            case BiographyMatt:
                Debug.Log("matts bio added");
                summoningCircle.candleLitCount++;
                summoningCircle.LightCandle();
                ItemController.ClearItemInHand();
                break;
            case CheesyPickupLine:
                Debug.Log("pickup lines added");
                summoningCircle.candleLitCount++;
                summoningCircle.LightCandle();
                requirementsMet[2] = true;
                ItemController.ClearItemInHand();
                break;
            case ComedyDVD:
                Debug.Log("comedy dvd added");
                summoningCircle.candleLitCount++;
                summoningCircle.LightCandle();
                ItemController.ClearItemInHand();
                break;
            case PoemBook:
                Debug.Log("poem added");
                summoningCircle.candleLitCount++;
                summoningCircle.LightCandle();
                ItemController.ClearItemInHand();
                break;
            default:
                Debug.Log("fail");
                break;
        }

        if(summoningCircle.candleLitCount == 3)
        {
            StartCoroutine(GiveDvd(summoningCircle));
        }  
    }

    IEnumerator GiveDvd(SummoningCircleObj summoningCircle)
    {
        yield return new WaitForSeconds(2);

        int count = 0;
        for (int i = 0; i < requirementsMet.Length; i++)
        {
            if (requirementsMet[i] == true)
                count++;
        }

        if (count == 0)
        {
            ItemController.AddItemToHand(thrillingDVD);
        }
        else if (count <= 2)
        {
            ItemController.AddItemToHand(funDVD);
        }
        else if (count == 3)
        {
            ItemController.AddItemToHand(strangeDVD);
        }

        requirementsMet[0] = false;
        requirementsMet[1] = false;
        requirementsMet[2] = false;

        yield return new WaitForSeconds(1);
        summoningCircle.ResetCandles();
    }
    public void ClickedTV()
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        itemInHand = ItemController.itemInHand;
        switch(itemInHand)
        {
            case FunDVD:
                Debug.Log("fun dvd");
                break;
            case ThrillingDVD:
                Debug.Log("thrilling dvd");
                break;
            case StrangeDVD:
                OnCompleted();
                Debug.Log("strange dvd");
                break;
            default:
                Debug.Log("fail");
                break;
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
