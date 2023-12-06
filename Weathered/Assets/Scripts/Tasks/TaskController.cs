using System;
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
    public AudioSource pageFlip;

    public GameObject taskScreenList;
    public GameObject taskObj;

    public string[] roomnames = new string[]{ "Entrance", "ChildrensToy", "DVDNBook", "ChinaNFurniture", "Doll", "CollectiblesNMemoirs", "Taxidermy", "CelebrityMerch", "Clock " };
    int selectedPage = 0;
    float selectionTimer = 0.2f;
    int times = 0;

    PlayerController player;

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
}
