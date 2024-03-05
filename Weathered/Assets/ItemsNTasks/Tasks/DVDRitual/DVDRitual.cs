using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DVDRitual : Task
{
    Item itemInHand;

    StrangeDVD strangeDVD;
    FunDVD funDVD;
    ThrillingDVD thrillingDVD;

    bool[] requirementsMet = {false, false, false};
    [SerializeField] GameObject[] objects2Dis;
    [SerializeField] Light2D tvSpiritLight;

    SummoningCircleObj summoningCircle;
    [SerializeField] GameObject[] tvStages;

    public override void InstanceTask()
    {
        base.InstanceTask();
        strangeDVD = FindAnyObjectByType<StrangeDVD>();
        funDVD = FindAnyObjectByType<FunDVD>();
        thrillingDVD = FindAnyObjectByType<ThrillingDVD>();
        summoningCircle = FindAnyObjectByType<SummoningCircleObj>();

        HandleTvStage(0);
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
                //Handle upset dialog here
                HandleTvStage(1);
                StartCoroutine(AngryDialog());
                break;
            case ThrillingDVD:
                HandleTvStage(3);
                Debug.Log("player has died");
                break;
            case StrangeDVD:
                objects2Dis[0].SetActive(false);
                objects2Dis[1].SetActive(false);
                objects2Dis[2].SetActive(false);
                tvSpiritLight.intensity = 1.5f;
                tvSpiritLight.pointLightOuterRadius = 15;
                summoningCircle.gameObject.GetComponent<Interactable>().enabled = false;
                HandleTvStage(2);
                OnCompleted();
                Debug.Log("strange dvd");
                break;
            default:
                Debug.Log("fail");
                break;
        }

    }
    IEnumerator AngryDialog()
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll1, "I am upset cuz you put in the wrong movie but i wont kill you :)");
        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
    void HandleTvStage(int num)
    {
        for(int i = 0; i < tvStages.Length; i++)
        {
            if(i != num)
                tvStages[i].SetActive(false);
            else
                tvStages[i].SetActive(true);
        }
    }
    public override void OnFailed()
    {
        //trigger death condition
        Debug.Log("player has died");
    }

    public override void LoadFinishedTask()
    {
        objects2Dis[0].SetActive(false);
        objects2Dis[1].SetActive(false);
        objects2Dis[2].SetActive(false);
        tvSpiritLight.intensity = 1.5f;
        tvSpiritLight.pointLightOuterRadius = 15;
        summoningCircle.gameObject.GetComponent<Interactable>().enabled = false;
        HandleTvStage(2);
        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}