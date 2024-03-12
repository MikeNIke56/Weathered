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
    [SerializeField] GameObject[] spiritTvStages;

    [SerializeField] AudioSource tvStatic;

    public override void InstanceTask()
    {
        base.InstanceTask();
        strangeDVD = FindAnyObjectByType<StrangeDVD>();
        funDVD = FindAnyObjectByType<FunDVD>();
        thrillingDVD = FindAnyObjectByType<ThrillingDVD>();
        summoningCircle = FindAnyObjectByType<SummoningCircleObj>();

        HandleTvStage(0);
        HandleSpiritTvStage(0);
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
                summoningCircle.LightCandle();
                summoningCircle.candleLitCount++;
                requirementsMet[0] = true;
                ItemController.ClearItemInHand();
                break;
            case ArvensHits:
                Debug.Log("arvens hits added");
                summoningCircle.LightCandle();
                summoningCircle.candleLitCount++;
                requirementsMet[1] = true;
                ItemController.ClearItemInHand();
                break;
            case BiographyMatt:
                Debug.Log("matts bio added");
                summoningCircle.LightCandle();
                summoningCircle.candleLitCount++;
                ItemController.ClearItemInHand();
                break;
            case CheesyPickupLine:
                Debug.Log("pickup lines added");
                summoningCircle.LightCandle();
                summoningCircle.candleLitCount++;
                requirementsMet[2] = true;
                ItemController.ClearItemInHand();
                break;
            case ComedyDVD:
                Debug.Log("comedy dvd added");
                summoningCircle.LightCandle();
                summoningCircle.candleLitCount++;
                ItemController.ClearItemInHand();
                break;
            case PoemBook:
                Debug.Log("poem added");
                summoningCircle.LightCandle();
                summoningCircle.candleLitCount++;
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
        summoningCircle.ResetCircle();
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
                //Handle upset dialog here
                HandleTvStage(1);
                StartCoroutine(AngryDialog());
                ItemController.ClearItemInHand();
                tvStatic.Stop();
                break;
            case ThrillingDVD:
                HandleTvStage(3);
                tvStatic.Stop();
                break;
            case StrangeDVD:
                DisableObjects();
                tvSpiritLight.intensity = 1.5f;
                tvSpiritLight.pointLightOuterRadius = 15;
                summoningCircle.gameObject.GetComponent<Interactable>().enabled = false;
                HandleTvStage(2);
                HandleSpiritTvStage(1);
                OnCompleted();
                Progression.Prog.DVDDolls();
                ItemController.ClearItemInHand();
                tvStatic.Stop();
                break;
            default:
                Debug.Log("interacted with tv");
                break;
        }

    }
    IEnumerator AngryDialog()
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll1, "This movie sucks!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll2, "In a bad way!");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.TvDoll3, "It's not all bad, I guess...");
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
    void HandleSpiritTvStage(int num)
    {
        for (int i = 0; i < spiritTvStages.Length; i++)
        {
            if (i != num)
                spiritTvStages[i].SetActive(false);
            else
                spiritTvStages[i].SetActive(true);
        }
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
            else if(objects2Dis[i].tag == "Interactable")
                objects2Dis[i].tag = "Untagged";
        }
    }

    public override void LoadFinishedTask()
    {
        DisableObjects();
        tvSpiritLight.intensity = 1.5f;
        tvSpiritLight.pointLightOuterRadius = 15;
        summoningCircle.gameObject.GetComponent<Interactable>().enabled = false;
        HandleTvStage(2);
        HandleSpiritTvStage(1);
        tvStatic.Stop();
        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
