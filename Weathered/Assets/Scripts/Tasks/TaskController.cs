using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskController : MonoBehaviour, ISavable
{
    static public TaskController taskControl;
    public List<Task> taskList = new List<Task>(); //Full list of every task possible
    public AudioSource taskCompleteAudio;
    public AudioSource taskBadActionAudio;
    public AudioSource pageFlip;

    public GameObject taskScreenList;
    public GameObject taskObj;

    public string[] roomnames = new string[] { "Entrance", "ChildrensToy", "DVDNBook", "ChinaNFurniture", "CollectiblesNMemoirs", "Taxidermy", "CelebrityMerch", "Mazarine", "Aunt" };
    int selectedPage = 0;
    int times = 0;
    int avialableTasks = 0;

    [SerializeField] GameObject keyDVDReward;
    [SerializeField] Transform keyDVDLocation;

    PlayerController player;
    [SerializeField] GameObject[] arrows;

    private void Awake()
    {
        taskControl = this;
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        //InstanceTasks();
    }

    void Update()
    {
        HandleUpdate();
    }

    public void InstanceTasks()
    {
        foreach (Task singleTask in taskList)
        {
            singleTask.InstanceTask();
        }

        while (times < 2)
        {
            UpdateList();
            times++;
        }
        times = 0;
    }
    public void UpdateList()
    {
        //clear existing task in list
        foreach (Transform slot in taskScreenList.transform)
            Destroy(slot.gameObject);

        //create new task slot
        foreach (var task in taskList)
        {
            if (task.noteBookPage == selectedPage && task.hasBeenDisc == true)
                task.AddToList();
        }
    }

    public void HandleUpdate()
    {
        if (selectedPage == 0)
        {
            arrows[0].gameObject.SetActive(false);
            arrows[1].gameObject.SetActive(true);
        }
        else if (selectedPage == avialableTasks - 1)
        {
            arrows[0].gameObject.SetActive(true);
            arrows[1].gameObject.SetActive(false);
        }
        else
        {
            arrows[0].gameObject.SetActive(true);
            arrows[1].gameObject.SetActive(true);
        }

    }

    public void IncrementPage()
    {
        if (player.state == PlayerController.GameState.Menu)
        {
            int prevPage = selectedPage;

            ++selectedPage;


            avialableTasks = 0;
            foreach (var task in taskList)
            {
                if (task.hasBeenDisc == true)
                    avialableTasks++;
            }
            selectedPage = Mathf.Clamp(selectedPage, 0, avialableTasks - 1);


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
        }
    }
    public void DecrementPage()
    {
        if (player.state == PlayerController.GameState.Menu)
        {
            int prevPage = selectedPage;

            --selectedPage;

            avialableTasks = 0;
            foreach (var task in taskList)
            {
                if (task.hasBeenDisc == true)
                    avialableTasks++;
            }
            selectedPage = Mathf.Clamp(selectedPage, 0, avialableTasks - 1);


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
        }
    }

    public void SetPage(int page)
    {
        selectedPage = page;
        UpdateList();
    }

    public void FindTasks()
    {
        taskList.Clear();
        foreach (var task in FindObjectsByType<Task>(FindObjectsSortMode.None))
            if(task.name != "SortToys")
                taskList.Add(task);
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
        FindTasks();
        var saveData = state as List<TaskSaveData>;
        if (saveData != null)
        {
            foreach (TaskSaveData task in saveData)
            {
                for (int i = 0; i < taskList.Count; i++)
                {
                    if (taskList[i].taskName == task.taskName)
                    {
                        taskList[i].SetTask(task);
                        taskList[i].timesFailed = 0;

                        if (taskList[i].currentState == Task.taskState.Completed)
                            taskList[i].LoadFinishedTask();
                    }
                }
            }
            Debug.Log("loaded");
        }
    }
}
