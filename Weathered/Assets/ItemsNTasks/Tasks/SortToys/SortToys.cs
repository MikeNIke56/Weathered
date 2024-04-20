using System.Collections.Generic;
using UnityEngine;

public class SortToys : Task
{
    public List<Item> validToys;
    [SerializeField] List<SortToyToy> toysList;
    int correctToys = 0;
    [SerializeField] AudioSource binPlaceLight;
    [SerializeField] AudioSource binPlaceHeavy;


    public override void InstanceTask()
    {
        base.InstanceTask();
        SetToys();
    }

    public void ClickToy(SortToyToy clickedToy)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }
        clickedToy.PickUpToy();
    }
    public void ClickBin(SortToyBin clickedBin)
    {

        if (currentState == taskState.Completed)
        {

        }
        else
        {
            if (ItemController.itemInHand == null)
            {

            }
            else if (validToys.Contains(ItemController.itemInHand))
            {
                if (currentState == taskState.Available)
                {
                    OnInProgress();
                }

                if (clickedBin.CheckToy(ItemController.itemInHand))
                {
                    ItemController.ClearItemInHand();
                    if (Random.Range(0, 2) >= 1)
                    {
                        binPlaceHeavy.Play();
                    }
                    else
                    {
                        binPlaceLight.Play();
                    }
                    correctToys++;
                    if (correctToys >= 9)
                    {
                        OnCompleted();
                    }
                }
                else
                {
                    ShortTextController.STControl.AddShortText("This isn't the right bin...");
                    TaskController.taskControl.taskBadActionAudio.Play();
                    foreach (SortToyToy toyObject in toysList)
                    {
                        toyObject.ResetThisToy(ItemController.itemInHand);
                    }
                    ItemController.ClearItemInHand();
                }
            }
            else
            {
                ShortTextController.STControl.AddShortText("This doesn't go in there...");
            }
        }
    }

    public override void LoadFinishedTask()
    {
        foreach (SortToyToy toy in toysList)
        {
            toy.gameObject.SetActive(false);
        }
        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }

    void SetToys()
    {
        if(GameManager.GM.hasReloaded == false)
            GameManager.GM.SetToyItems(validToys);
        else
        {
            validToys.Clear();
            foreach (var toy in GameManager.GM.GetToyItems())
                validToys.Add(toy);
        }

        foreach (var bin in FindObjectsByType<SortToyBin>(FindObjectsSortMode.None))
            bin.SearchToys();
    }
}
