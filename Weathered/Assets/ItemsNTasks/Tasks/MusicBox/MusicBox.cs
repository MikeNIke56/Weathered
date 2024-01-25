using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : Task
{
    [SerializeField] List<Item> validTapes;
    [SerializeField] MusicBoxBox musicBoxNormal;
    [SerializeField] MusicBoxBox musicBoxSpirit;
    [SerializeField] GameObject[] itemsToHide;


    public void ClickedTape(MusicBoxTape tapeClicked)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }
        ItemController.AddItemToHand(tapeClicked.GetTapeItem());
        Destroy(tapeClicked.gameObject);
    }
    public void ClickedBox(MusicBoxBox boxClicked)
    {
        if (currentState == taskState.Completed)
        {
            //TODO setup task: box prefab, cassette locations, AudioSources
        }
        else
        {
            if (boxClicked.currentCassette != null)
            {
                if (currentState == taskState.Available)
                {
                    OnInProgress();
                }
            }
            Item secondHand = ItemController.itemInHand;
            boxClicked.RetreiveTape();

            if (validTapes.Contains(secondHand))
            {
                boxClicked.InsertTape(secondHand);
                if (!boxClicked.isSpiritBox && validTapes.IndexOf(secondHand) == 2)
                {
                    OnCompleted();
                }
                else if (!boxClicked.isSpiritBox)
                {
                    OnFailed();
                    Debug.Log("Wrong cassette :sad face: you died.");
                }
            }

            if (boxClicked.isSpiritBox)
            {
                if (validTapes.IndexOf(boxClicked.currentCassette) != 3)
                {
                    SpiritWorldJump.SWJ.jumpBlockers["CassetteSpirit"] = true;
                }
                else
                {
                    SpiritWorldJump.SWJ.jumpBlockers["CassetteSpirit"] = false;
                }
            }
        }
    }
    public void JumpFailed()
    {
        Debug.Log("Mazarine died by leaving the dancing spirits without music...");
    }

    public override void LoadFinishedTask()
    {
        musicBoxNormal.RetreiveTape();
        musicBoxNormal.InsertTape(validTapes[2]);
        musicBoxSpirit.RetreiveTape();
        musicBoxSpirit.InsertTape(validTapes[3]);

        foreach (var item in itemsToHide)
        {
            item.SetActive(false);
        }

        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
