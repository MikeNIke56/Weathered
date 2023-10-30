using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour
{
    static public TaskController taskControl;
    public List<Task> taskList = new List<Task>(); //Full list of every task possible
    public List<Task> taskChosenList = new List<Task>(); //Task list for chosen tasks this game. Referenced commonly. Auto-populated with curation.
    public AudioSource taskCompleteAudio;
    public AudioSource taskBadActionAudio;

    [SerializeField] GameObject taskScreenList;
    [SerializeField] GameObject taskObj;

    void Start()
    {
        if (taskControl == null)
        {
            taskControl = FindFirstObjectByType<TaskController>();
        }
        //deletes placeholder objects in the parent
        //foreach(Transform obj in taskScreenList.transform)
        //{
        //    Destroy(obj.gameObject);
        //}
        foreach (Task singleTask in taskList)
        {
            singleTask.InstanceTask();

            //adds all tasks to the task screen
            //Instantiate(taskObj, taskScreenList.transform);
            //taskObj.GetComponentInChildren<Text>().text = singleTask.name + ": " + singleTask.description;
        }
    }
}
