using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDetermine : MonoBehaviour
{
    [SerializeField] TaskBase[] tasks;
    [SerializeField] TaskBase chosenTask;
    public bool isActive = false;

    private void Awake()
    {
        ChooseTask();
    }

    Task ChooseTask()
    {
        chosenTask = tasks[Random.Range(0, tasks.Length)];
        gameObject.GetComponent<SpriteRenderer>().sprite = chosenTask.OverWorldIcon;
        chosenTask.ChooseStartItem();
        return (Task)chosenTask;
    }


    public TaskBase ChosenTask => chosenTask;
}
