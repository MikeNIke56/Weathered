using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDetermine : MonoBehaviour
{
    [SerializeField] TaskBase[] tasks;
    [SerializeField] TaskBase chosenTask;

    private void Awake()
    {
        ChooseTask();
    }

    TaskBase ChooseTask()
    {
        chosenTask = tasks[Random.Range(0, tasks.Length)];
        gameObject.GetComponent<SpriteRenderer>().sprite = chosenTask.OverWorldIcon;
        return chosenTask;
    }

    public TaskBase ChosenTask => chosenTask;
}
