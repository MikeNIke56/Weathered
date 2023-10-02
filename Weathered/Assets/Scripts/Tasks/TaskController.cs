using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] TaskObj[] tasks;
    [SerializeField] public TaskObj taskObj;
    [SerializeField] GameObject taskList;

    [SerializeField] TaskBase[] tasksBaseList;

    bool started = false;

    private void Awake()
    {
        EmptyList();
    }

    void Start()
    {
        if (started == false)
        {
            GiveTasks();
        }

    }

    void Update()
    {
        
    }

    void EmptyList()
    {
        //clear existing item in list

        if (taskList.GetComponentsInChildren<TaskObj>().Length > 0)
        {
            foreach (TaskObj obj in taskList.gameObject.GetComponentsInChildren<TaskObj>())
                Destroy(obj.gameObject);
        }
    }

    public void AddTask(TaskBase task, int index)
    {
        var taskObjCopy = Instantiate(taskObj, taskList.transform);
        taskObjCopy.SetData(task);
     
        tasks[index] = taskObjCopy;
        tasksBaseList[index] = task;
    }

    public void RemoveTask(TaskObj task)
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            if (tasks[i] == task)
            {
                tasks[i] = null;
                tasksBaseList[i] = null;
                break;
            }
        }
        Destroy(task.gameObject);
    }

    void GiveTasks()
    {
        var items = FindObjectsByType<ItemDetermine>(FindObjectsSortMode.None);

        for (int i = 0; i < items.Length; i++)
        {
            AddTask(items[i].ChosenItem.Task, i);
        }
        started = true;
    }

    public TaskBase[] TasksBaseList => tasksBaseList;
}
