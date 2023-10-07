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

    TaskController taskController;
    ItemHUD itemHUD;

    PlayerController player;

    Vector2 spawnPosition;

    bool started = false;
    public static TaskController i { get; private set; }

    private void Awake()
    {
        EmptyList();
        i = this;
        player = FindAnyObjectByType<PlayerController>(FindObjectsInactive.Include);
        taskController = FindAnyObjectByType<TaskController>(FindObjectsInactive.Include);
        itemHUD = FindAnyObjectByType<ItemHUD>(FindObjectsInactive.Include);

         
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

    public void BeginTask(Task task)
    {
        //start task
        var tasks = FindObjectsByType<TaskDetermine>(FindObjectsSortMode.None);

        foreach (TaskDetermine taskD in tasks)
        {
            if (taskD.isActive == true)
            {
                Debug.Log("A task is already active");
                return;
            }

            foreach (TaskBase taskB in tasksBaseList)
            {
                if (taskD.ChosenTask == taskB)
                {
                    if (taskD.isActive == true)
                    {
                        Debug.Log("A task is already active");
                        return;
                    }
                    else
                    {
                        if (taskB == task)
                        {
                            task.StartTask();
                            player.curItem = task.StartItem;
                            itemHUD.SetImage(task.StartItemIcon);

                            for (int i = 0; i < task.itemsToSpawn.Length; i++)
                            {
                                var itemCopy = Instantiate(task.itemObj);
                                //itemCopy.GetComponent<ItemDetermine>().ChooseItem(task.ItemsToSpawn[i]);
                            }

                            taskD.isActive = true;
                        }
                    }
                }
            }
        }
                 
  
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

    /*void ResetTasks()
    {
        var tasks = FindObjectsByType<TaskDetermine>(FindObjectsSortMode.None);

        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i].ChosenTask().
        }
    }*/

    Vector2 PickRandSpawnPos()
    {
        var playerPos = player.transform.position;

        while(spawnPosition.x > player.spawnRng)
        {

        }

        return spawnPosition;
    }

    public TaskBase[] TasksBaseList => tasksBaseList;
}
