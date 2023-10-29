using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    static public TaskController taskControl;
    public List<Task> taskList = new List<Task>(); //Full list of every task possible
    public List<Task> taskChosenList = new List<Task>(); //Task list for chosen tasks this game. Referenced commonly. Auto-populated with curation.
    public AudioSource taskCompleteAudio;
    public AudioSource taskBadActionAudio;

    void Start()
    {
        if (taskControl == null)
        {
            taskControl = FindFirstObjectByType<TaskController>();
        }
        foreach (Task singleTask in taskList)
        {
            singleTask.InstanceTask();
        }
    }
}
