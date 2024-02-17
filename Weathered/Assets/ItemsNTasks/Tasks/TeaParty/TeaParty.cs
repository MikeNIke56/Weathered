using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaParty : Task
{
    public bool deathTriggered = false;
    bool isTeaPlaced = false;

    [SerializeField] PlayerController player;

    [SerializeField] Cookies cookies;
    [SerializeField] TeaSet teaSet;
    [SerializeField] Teabags teabags;
    [SerializeField] Kettle kettle;

    [SerializeField] Transform cookiesPos;
    [SerializeField] Transform teaSetPos;
    [SerializeField] Transform teabagsPos;
    [SerializeField] Transform kettlePos;

    Pantry pantry;
    TeaCabinet teaCabinet;
    TeabagsCabinet teabagsCabinet;

    bool[] itemsPlaced = {false, false, false, false};

    private void Awake()
    {
        pantry = FindAnyObjectByType<Pantry>();
        teaCabinet = FindAnyObjectByType<TeaCabinet>();
        teabagsCabinet = FindAnyObjectByType<TeabagsCabinet>();
    }
    private void Update()
    {
        if(deathTriggered==true)
            OnFailed();
    }

    public override void InstanceTask()
    {
        base.InstanceTask();

    }
    public void ClickedTable(Interaction interaction)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        if (ItemController.itemInHand == teaSet && isTeaPlaced == false)
        {
            isTeaPlaced = true;
            teaSet.teaSetObject.gameObject.transform.position = teaSetPos.position;
            teaSet.teaSetObject.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "InFront";
            teaSet.teaSetObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            teaSet.teaSetObject.gameObject.GetComponent<Interactable>().enabled = false;
            ItemController.ClearItemInHand();
            CheckOffASpot(0);
        }
        else
        {
            if(isTeaPlaced == false && ItemController.itemInHand != null)
            {
                ShortTextController.STControl.AddShortText("I need to get plates and cups first.");
            }
            else
            {
                switch (ItemController.itemInHand)
                {
                    case Cookies:
                        cookies.cookiesObject.gameObject.transform.position = cookiesPos.position;
                        cookies.cookiesObject.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "InFront";
                        cookies.cookiesObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
                        pantry.canStillInteract = false;
                        cookies.cookiesObject.gameObject.GetComponent<Interactable>().enabled = false;
                        ItemController.ClearItemInHand();
                        CheckOffASpot(1);
                        CheckSpots();
                        break;
                    case Kettle:
                        kettle.kettleObject.gameObject.transform.position = kettlePos.position;
                        kettle.kettleObject.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "InFront";
                        kettle.kettleObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
                        kettle.kettleObject.gameObject.GetComponent<Interactable>().enabled = false;
                        ItemController.ClearItemInHand();
                        CheckOffASpot(2);
                        CheckSpots();
                        break;
                    case Teabags:
                        teabags.teaBagsObject.gameObject.transform.position = teabagsPos.position;
                        teabags.teaBagsObject.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "InFront";
                        teabags.teaBagsObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
                        teabagsCabinet.canStillInteract = false;
                        teabags.teaBagsObject.gameObject.GetComponent<Interactable>().enabled = false;
                        ItemController.ClearItemInHand();
                        CheckOffASpot(3);
                        CheckSpots();
                        break;
                    default:
                        Debug.Log("failed to place item");
                        break;
                }
            }
        }
    }

    void CheckSpots()
    {
        bool isDone = true;

        foreach(var val in itemsPlaced)
        {
            if(val == false)
                isDone = false;
        }

        if(isDone == true)
            OnCompleted();
    }

    void CheckOffASpot(int spot)
    {
        itemsPlaced[spot] = true;
    }



    public override void OnFailed()
    {
        //trigger death condition
        Debug.Log("player has died");

        player.moveBlockers["CutScene"] = true;
        ShortTextController.STControl.AddShortText("Oh no, it broke! Aunt will be mad…");
    }

    public override void LoadFinishedTask()
    {
        teaSet.teaSetObject.gameObject.transform.position = teaSetPos.position;
        cookies.cookiesObject.gameObject.transform.position = cookiesPos.position;
        kettle.kettleObject.gameObject.transform.position = kettlePos.position;
        teabags.teaBagsObject.gameObject.transform.position = teabagsPos.position;


        teaSet.teaSetObject.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "InFront";
        teaSet.teaSetObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        teaSet.teaSetObject.gameObject.GetComponent<Interactable>().enabled = false;

        cookies.cookiesObject.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "InFront";
        cookies.cookiesObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        cookies.cookiesObject.gameObject.GetComponent<Interactable>().enabled = false;

        kettle.kettleObject.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "InFront";
        kettle.kettleObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        kettle.kettleObject.gameObject.GetComponent<Interactable>().enabled = false;

        teabags.teaBagsObject.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "InFront";
        teabags.teaBagsObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        teabags.teaBagsObject.gameObject.GetComponent<Interactable>().enabled = false;

        teaCabinet.hasGiven = true;
        pantry.canStillInteract = false;
        teabagsCabinet.canStillInteract = false;

        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
