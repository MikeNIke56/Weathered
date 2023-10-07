using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] TaskObj[] tasks;
    [SerializeField] public TaskObj taskObj;
    [SerializeField] GameObject taskList;

    [SerializeField] TaskBase[] tasksBaseList;
    InteractionMenu interactionMenu;

    bool started = false;
    public static TaskController i { get; private set; }

    private void Awake()
    {
        EmptyList();
        i = this;
    }

    void Start()
    {
        interactionMenu = FindAnyObjectByType<InteractionMenu>(FindObjectsInactive.Include);

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
        var tasks = FindObjectsByType<TaskDetermine>(FindObjectsSortMode.None);

        for (int i = 0; i < tasks.Length; i++)
        {
            AddTask(tasks[i].ChosenTask, i);
        }
        started = true;
    }

    public void StartTask(Task task)
    {
        task.StartTask();
    }

    public void DisplayTask(Task task)
    {
        bool taskClicked = task.Display();

        if (taskClicked)
        {
            interactionMenu.gameObject.SetActive(true);
            interactionMenu.DisplayInfo(task);
        }
    }

    public TaskBase[] TasksBaseList => tasksBaseList;
}
