using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTask : MonoBehaviour
{
    [SerializeField] InteractionMenu interactionMenu;
    TaskController taskController;
    ItemHUD itemHUD;

    PlayerController player;

    Vector2 spawnPosition;

    public static StartTask i { get; private set; }


    private void Awake()
    {
        i = this;
        player = FindAnyObjectByType<PlayerController>(FindObjectsInactive.Include);
        taskController = FindAnyObjectByType<TaskController>(FindObjectsInactive.Include);
        itemHUD = FindAnyObjectByType<ItemHUD>(FindObjectsInactive.Include);
    }

    private void Start()
    {

    }

    public void BeginTask(TaskBase task)
    {
        //adds task to task list

        itemHUD.SetImage(task.StartItemIcon);

        foreach (TaskBase taskB in taskController.TasksBaseList)
        {
            if (taskB == task)
            {
                task.StartTask();

                for (int i = 0; i < task.ItemsToSpawn.Length; i++)
                {
                    var itemCopy = Instantiate(task.itemObj);
                    //itemCopy.GetComponent<ItemDetermine>().ChooseItem(task.ItemsToSpawn[i]);
                }

                player.curItem = task.StartItem;
            }
        }
    }

    void PickRandSpawnPos()
    {
        //float xVal = .transform.position.x - player.transform.position.x;
        //float yVal = obj.collider.gameObject.transform.position.y - player.transform.position.y;
    }
}
