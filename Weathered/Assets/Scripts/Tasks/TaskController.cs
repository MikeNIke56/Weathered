using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class TaskController : MonoBehaviour
{
    [SerializeField] TaskObj[] tasks;
    [SerializeField] public TaskObj taskObj;
    [SerializeField] GameObject taskList;

    [SerializeField] TaskBase[] tasksBaseList;
    InteractionMenu interactionMenu;

    ItemHUD itemHUD;

    PlayerController player;

    //[SerializeField] float minSpawnDist;
    //[SerializeField] float spawnRng;
    GameObject[] boxPositions = new GameObject[3];
    public Item[] emptyBoxItems;

    bool started = false;
    //TaskDetermine taskDetermine;

    public static TaskController i { get; private set; }

    private void Awake()
    {
        EmptyList();
        i = this;
        player = FindAnyObjectByType<PlayerController>(FindObjectsInactive.Include);
        itemHUD = FindAnyObjectByType<ItemHUD>(FindObjectsInactive.Include);    
        //taskDetermine = FindAnyObjectByType<TaskDetermine>(FindObjectsInactive.Include);
    }

    void Start()
    {
        interactionMenu = FindAnyObjectByType<InteractionMenu>(FindObjectsInactive.Include);

        if (started == false)
        {
            GiveTasks();
        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("BoxesSpawner").Length; i++)
        {
            boxPositions[i] = GameObject.FindGameObjectsWithTag("BoxesSpawner")[i];
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
            if (taskD.isActive == true && taskD.ChosenTask == task && taskD.ChosenTask.returnable == false)
            {
                Debug.Log("You've already started this task");
                return;
            }
            else if (taskD.isActive == true && taskD.ChosenTask.returnable == false)
            {
                Debug.Log("A task is already active");
                return;
            }

            foreach (TaskBase taskB in tasksBaseList)
            {
                if (taskD.ChosenTask == taskB)
                {
                    if (taskB == task)
                    {
                        task.StartTask();
                        //player.curItem = task.StartItem;
                        //itemHUD.SetImage(task.StartItemIcon);

                        var taskItems = task.itemsToSpawn;

                        switch (task.Name)
                        { 
                            case "Sort Boxes":

                                if(TaskDetermine.i.getIsActive(taskD) == false)
                                {
                                    for (int i = 0; i < taskItems.Length; i++)
                                    {
                                        var itemCopy = Instantiate(task.itemObj, boxPositions[i].transform);
                                        itemCopy.GetComponent<ItemDetermine>().SetItem(task.itemsToSpawn[i]);
                                        itemCopy.GetComponent<ItemDetermine>().SetBoxReactItem(task.PossibleBoxItemsList);

                                        emptyBoxItems[i] = itemCopy.GetComponent<ItemDetermine>().BoxSortItem;
                                    }

                                    var chosenItem = emptyBoxItems[Random.Range(0, emptyBoxItems.Length)];
                                    player.curItem = chosenItem;
                                    itemHUD.SetImage(chosenItem.OverWorldIcon);
                                }
                                else if(TaskDetermine.i.getIsActive(taskD) == true && player.curItem == null)
                                {
                                    for (int i = 0; i < emptyBoxItems.Length; i++)
                                    {
                                        if (emptyBoxItems[i] != null)
                                        {
                                            player.curItem = emptyBoxItems[i];
                                            itemHUD.SetImage(emptyBoxItems[i].OverWorldIcon);
                                            return;
                                        }
                                    }

                                    TaskDetermine.i.setIsCompleted(taskD);

                                    if (TaskDetermine.i.getIsCompleted(taskD) == true)
                                        TaskDetermine.i.Complete(taskD);
                                }

                                /*for (int i = 0; i > emptyBoxItems.Length; i++)
                                {
                                    if (emptyBoxItems[i] == chosenItem)
                                        emptyBoxItems[i] = null;
                                }*/

                                break;
                            case "Task":
                                //TaskController.i.BeginTask((Task)obj.collider.gameObject.GetComponent<TaskDetermine>().ChosenTask);
                                break;
                            case "Item":
                                //ItemController.i.HandleItem((Item)obj.collider.gameObject.GetComponent<ItemDetermine>().ChosenItem);
                                break;
                            default:
                                Debug.Log("test");
                                break;
                        }   

                        taskD.isActive = true;
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

    Transform SpawnBoxes(GameObject spawnObj, GameObject parentObj, int i)
    {
        var listofSpawns = parentObj.GetComponentsInChildren<Transform>();

        spawnObj.transform.position = listofSpawns[i].position;
        return spawnObj.transform;

        /*float randomRange = Random.Range(-spawnRng, spawnRng);
        Vector3 randomPosition = new Vector3(randomRange, transform.position.y, transform.position.z);

        if(Mathf.Abs(randomRange) <= minSpawnDist)
        {
            PickRandSpawnPos(obj);
        }

        obj.transform.position = randomPosition;
        return obj.transform;*/
    }

    public TaskBase[] TasksBaseList => tasksBaseList;
}
