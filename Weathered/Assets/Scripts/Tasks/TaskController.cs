using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour, ISavable
{
    static public TaskController taskControl;
    public List<Task> taskList = new List<Task>(); //Full list of every task possible
    public AudioSource taskCompleteAudio;
    public AudioSource taskBadActionAudio;
    public AudioSource pageFlip;

    public GameObject taskScreenList;
    public GameObject taskObj;

    public string[] roomnames = new string[]{ "Entrance", "ChildrensToy", "DVDNBook", "ChinaNFurniture", "CollectiblesNMemoirs", "Taxidermy", "CelebrityMerch", "Mazarine", "Aunt" };
    int selectedPage = 0;
    float selectionTimer = 0.2f;
    int times = 0;

    [SerializeField] GameObject keyDVDReward;
    [SerializeField] Transform keyDVDLocation;

    PlayerController player;

    private void Awake()
    {
        taskControl = this;
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        if (taskControl == null)
            taskControl = FindFirstObjectByType<TaskController>();

        foreach (Task singleTask in taskList)
            singleTask.InstanceTask();

        while(times < 2)
        {
            UpdateList();
            times++;
        }    
        times = 0;
    }

    void Update()
    {
        HandleUpdate();
    }

    void UpdateList()
    {
        //clear existing task in list
        foreach (Transform slot in taskScreenList.transform)
            Destroy(slot.gameObject);

        //create new task slot
        foreach (var task in taskList)
        {
            if (task.room.ToString() == roomnames[selectedPage])
                task.AddToList();
        }

    }
    public void HandleUpdate()
    {
        if(player.state == PlayerController.GameState.Menu)
        {
            int prevPage = selectedPage;

            if (Input.GetKeyDown(KeyCode.D) && selectionTimer == 0)
            {
                ++selectedPage;
                selectionTimer = 0.2f;
            }                
            else if (Input.GetKeyDown(KeyCode.A) && selectionTimer == 0)
            {
                --selectedPage;
                selectionTimer = 0.2f;
            }

            try
            {
                selectedPage = Mathf.Clamp(selectedPage, 0, roomnames.Length - 1);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            if (prevPage != selectedPage && selectedPage >= 0)
            {
                while (times < 2)
                {
                    UpdateList();
                    times++;
                }
                times = 0;
                pageFlip.Play();
            }

            UpdateTimer();
        }
    }

    void UpdateTimer()
    {
        if (selectionTimer > 0)
        {
            selectionTimer = Mathf.Clamp(selectionTimer - Time.deltaTime, 0, selectionTimer);
        }
    }

    public void CheckCompleteTasks()
    {
        bool isAllComplete = true;
        Debug.Log("Checking Tasks...");
        foreach (Task singleTask in taskList)
        {
            if (singleTask.currentState != Task.taskState.Completed)
            {
                Debug.Log(singleTask.taskName + " is incomplete.");
                isAllComplete = false;
            }
        }
        if (isAllComplete)
        {
            Debug.Log("All complete!");
            Instantiate(keyDVDReward, keyDVDLocation);
        }
    }

    public object CaptureState()
    {
        return taskList.Select(q => q.GetSaveData()).ToList();
    }

    public void RestoreState(object state)
    {
        var saveData = state as List<TaskSaveData>;
        if(saveData != null)
        {
            foreach (TaskSaveData task in saveData)
            {
                for(int i = 0; i < taskList.Count; i++)
                {
                    if (taskList[i].taskName == task.taskName)
                    {
                        taskList[i].SetTask(task);

                        if(taskList[i].currentState == Task.taskState.Completed)
                            taskList[i].LoadFinishedTask();
                    }
                }
            }
            Debug.Log("loaded");
        }
    }
}
