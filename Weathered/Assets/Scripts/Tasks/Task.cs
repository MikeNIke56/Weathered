using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Task : Interaction
{
    public string taskName;
    public string description;
    public string shortName;
    public string shortDescription;
    public string voicemailDescription;
    public string voicemailShortQuote;
    public string hintText;
    public enum taskState { None, Available, InProgress, Completed }
    public taskState currentState;
    public bool isFailed = false;
    public bool isAlwaysChosen = false;
    public GameObject taskToggleRoot; // Gameobject child to activate/deactivate task.
    public GameObject taskIconPrefab; // For task journal, Not Started / default. Prefabs to allow scripting and animation
    public GameObject taskInProgressIconPrefab; // In progress icon
    public GameObject taskCompletedIconPrefab; // Completed icon
    public int timesFailed = 0;
    public bool hintGiven = false;
    public bool hasBeenDisc = false;
    public int noteBookPage;

    public enum taskRoom { Entrance, ChildrensToy, DVDNBook, ChinaNFurniture, CollectiblesNMemoirs, Taxidermy, CelebrityMerch, Mazarine, Aunt }
    public taskRoom room;

    public void SetTask(TaskSaveData saveData)
    {
        this.taskName = saveData.taskName;
        this.currentState = (taskState)saveData.currentState;
        this.voicemailDescription = saveData.voicemailDescription;
        this.voicemailShortQuote = saveData.voicemailShortQuote;
        this.isAlwaysChosen = saveData.isAlwaysChosen;
        this.isFailed = saveData.isFailed;
        this.timesFailed = saveData.timesFailed;
        this.hintGiven = saveData.hintGiven;
        this.noteBookPage = saveData.noteBookPage;
        this.hasBeenDisc = saveData.hasBeenDisc;
        this.room = (taskRoom)saveData.room;
        this.currentState = (taskState)saveData.currentState;
    }

    public virtual void InstanceTask()
    {
        if (taskToggleRoot != null)
        {
            taskToggleRoot.SetActive(true);
        }
        OnAvailable();
    }
    public virtual void OnAvailable()
    {
        currentState = taskState.Available;
    }
    public virtual void OnInProgress()
    {
        currentState = taskState.InProgress;
    }
    public virtual void OnCompleted()
    {
        this.currentState = taskState.Completed;
        FindAnyObjectByType<TaskController>().taskCompleteAudio.Play();
        FindAnyObjectByType<TaskController>().CheckCompleteTasks();
    }
    public virtual void LoadFinishedTask()
    {
    }

    public virtual void OnBadAction()
    {
        timesFailed++;
        if (timesFailed >= 3)
        {
            OnFailed();
        }

        FindAnyObjectByType<TaskController>().taskBadActionAudio.Play();
    }
    public virtual void OnFailed()
    {
        //trigger death condition
    }

    public virtual void AddToList()
    {
        Instantiate(TaskController.taskControl.taskObj, TaskController.taskControl.taskScreenList.transform);
        TaskController.taskControl.taskObj.GetComponentInChildren<Text>().text = taskName + ": \n" + description + "\n\n";

        if (hintGiven == true)
            TaskController.taskControl.taskObj.GetComponentInChildren<Text>().text += hintText;
    }


    public TaskSaveData GetSaveData()
    {
        var saveData = new TaskSaveData()
        {
            taskName = taskName,
            description = description,
            voicemailDescription = voicemailDescription,
            voicemailShortQuote = voicemailShortQuote,
            isAlwaysChosen = isAlwaysChosen,
            isFailed = isFailed,
            timesFailed = timesFailed,
            hintGiven = hintGiven,
            noteBookPage = noteBookPage,
            hasBeenDisc = hasBeenDisc,
            room = (TaskSaveData.taskRoom)room,
            currentState = (TaskSaveData.taskState)currentState
        };
        return saveData;
    }
}

[System.Serializable]
public class TaskSaveData
{
    public string taskName;
    public string description;
    public string voicemailDescription;
    public string voicemailShortQuote;
    public enum taskState { None, Available, InProgress, Completed }
    public taskState currentState;
    public bool isFailed = false;
    public bool isAlwaysChosen = false;
    public int timesFailed = 0;
    public bool hintGiven = false;
    public bool hasBeenDisc = false;
    public int noteBookPage;

    public enum taskRoom { Entrance, ChildrensToy, DVDNBook, ChinaNFurniture, Doll, CollectiblesNMemoirs, Taxidermy, CelebrityMerch, Clock }
    public taskRoom room;
}
