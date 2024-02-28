using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixElevator : Task
{
    PlayerController player;
    Fuse fuse;
    [SerializeField] FuseBox fuseBox;

    public enum FuseBoxState { Closed, Open, Fixed };
    public FuseBoxState state;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
        fuse = FindAnyObjectByType<Fuse>();
    }

    public override void InstanceTask()
    {
        base.InstanceTask();
    }
    public void ClickedFuseBox(Interaction interaction)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        if(state == FuseBoxState.Closed)
        {
            state = FuseBoxState.Open;
            interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[0].SetActive(false);
            interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[1].SetActive(true);
            interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[2].SetActive(false);

            ShortTextController.STControl.AddShortText("The elevator is broken! I can’t go upstairs!");
            return;
        }
        else if (ItemController.itemInHand is Fuse)
        {
            if(state == FuseBoxState.Open)
            {
                state = FuseBoxState.Fixed;
                interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[0].SetActive(false);
                interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[1].SetActive(false);
                interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[2].SetActive(true);


                ItemController.itemInHand.ClearItem();
                ItemController.itemInHand.gameObject.GetComponent<Fuse>().fuseObject.gameObject.SetActive(false);

                OnCompleted();
            }
            else if (state == FuseBoxState.Fixed)
            {
                Debug.Log("taking elevator up");
                var playerPos = player.gameObject.transform.position.y;
                playerPos += 20;
            }
        } 
    }


    public override void OnFailed()
    {
        //trigger death condition
        Debug.Log("player has died");
    }

    public override void LoadFinishedTask()
    {
        state = FuseBoxState.Fixed;
        fuseBox.fuseBoxObjs[0].SetActive(false);
        fuseBox.fuseBoxObjs[1].SetActive(false);
        fuseBox.fuseBoxObjs[2].SetActive(true);

        fuse.fuseObject.gameObject.SetActive(false);

        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
