using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] InteractionMenu interactionMenu;
    TaskController taskController;
    ItemHUD itemHUD;

    private void Awake()
    {
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

        itemHUD.SetImage(interactionMenu.Item.ItemIcon);

        foreach(TestTask task in taskController.TasksBaseList)
        {
            if(task == interactionMenu.Item.Task)
                task.StartTask();
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
