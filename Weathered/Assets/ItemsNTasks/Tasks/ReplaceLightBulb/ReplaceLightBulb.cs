using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ReplaceLightBulb : Task
{
    public Light2D normalLight;
    public Light2D spiritLight;

    [SerializeField] Lightbulb lightbulb;
    [SerializeField] StepStool stepStool;

    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    float time;
    int num;

    public bool stoolPlaced = false;

    [SerializeField] Transform stoolPos;
    [SerializeField] StepStoolObj stoolObj;

    public override void InstanceTask()
    {
        base.InstanceTask();     
    }

    void Start()
    {
        SetTime();
        OnInProgress();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != taskState.Completed)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                Flicker();
            }
        }
        else
        {
            normalLight.enabled = true;
            spiritLight.gameObject.SetActive(false);
        }
        
    }

    public void LightClicked()
    {
        if (currentState == taskState.Available)
            currentState = taskState.InProgress;

        if (currentState == taskState.InProgress)
        {
            if (ItemController.itemInHand == stepStool)
            {
                stoolObj = Instantiate(stoolObj, stoolPos);
                stoolObj.gameObject.SetActive(true);
                stoolPlaced = true;
                ItemController.ClearItemInHand();
            }
            if (ItemController.itemInHand is Lightbulb && stoolObj.onStool == true)
            {
                OnCompleted();
                FindFirstObjectByType<ClosetInteract>().isLocked = false;
                ItemController.ClearItemInHand();
            }
        }
    }


    void Flicker()
    {
        num = Random.Range(0, 100);

        if(num <= 15)
        {
            spiritLight.gameObject.SetActive(true);
            normalLight.enabled = false;
        }         
        else if(num <= 40)
        {
            normalLight.enabled = false;
            spiritLight.gameObject.SetActive(false);
        }
        else
        {           
            normalLight.enabled = true;
            spiritLight.gameObject.SetActive(false);
        }
        SetTime();
    }

    void SetTime()
    {
        time = Random.Range(minTime, maxTime);
    }

    public override void LoadFinishedTask()
    {
        normalLight.enabled = true;
        spiritLight.gameObject.SetActive(false);
        FindFirstObjectByType<ClosetInteract>().isLocked = false;
        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
