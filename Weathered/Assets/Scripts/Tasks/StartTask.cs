using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class StartTask : MonoBehaviour
{
    [SerializeField] InteractionMenu interactionMenu;
    TaskController taskController;
    ItemHUD itemHUD;

    PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>(FindObjectsInactive.Include);
        taskController = FindAnyObjectByType<TaskController>(FindObjectsInactive.Include);
        itemHUD = FindAnyObjectByType<ItemHUD>(FindObjectsInactive.Include);
    }

    private void Start()
    {

    }

    public void Yes()
    {
        //adds task to task list
        ResetElements();

        itemHUD.SetImage(interactionMenu.Task.StartItemIcon);

        foreach(TaskBase task in taskController.TasksBaseList)
        {
            if(task == interactionMenu.Task)
            {
                task.StartTask();
                player.curItem = task.StartItem;
            }
        }
    }

    public void No()
    {
        //exits interaction menu       
        ResetElements();
    }

    void ResetElements()
    {
        gameObject.SetActive(false);
        interactionMenu.gameObject.SetActive(false);
        interactionMenu.acceptIsShowing = false;
        interactionMenu.time = interactionMenu.maxTime;
        itemHUD.gameObject.SetActive(true);
    }

}
