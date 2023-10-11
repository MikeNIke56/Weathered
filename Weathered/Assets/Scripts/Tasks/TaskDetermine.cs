using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDetermine : MonoBehaviour
{
    [SerializeField] TaskBase[] tasks;
    [SerializeField] TaskBase chosenTask;
    public bool isActive = false;
    public bool isCompleted = false;

    public static TaskDetermine i { get; private set; }

    private void Awake()
    {
        i = this;
        ChooseTask();
    }

    Task ChooseTask()
    {
        chosenTask = tasks[Random.Range(0, tasks.Length)];
        gameObject.GetComponent<SpriteRenderer>().sprite = chosenTask.OverWorldIcon;
        chosenTask.ChooseStartItem();
        return (Task)chosenTask;
    }

    public bool getIsActive(TaskDetermine taskD)
    {
        return taskD.isActive;
    }

    public bool getIsCompleted(TaskDetermine taskD)
    {
        return taskD.isCompleted;
    }

    public void setIsCompleted(TaskDetermine taskD)
    {
        taskD.isCompleted = true;
    }

    public void Complete(TaskDetermine taskD)
    {
        taskD.gameObject.SetActive(false);
    }


    public TaskBase ChosenTask => chosenTask;
}
