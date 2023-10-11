using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    InteractionMenu interactionMenu;
    PlayerController player;

    TaskController taskController;
    ItemHUD itemHUD;

    public static ItemController i { get; private set; }

    private void Awake()
    {
        i = this;
        player = FindAnyObjectByType<PlayerController>();
    }

    private void Start()
    {
        interactionMenu = FindAnyObjectByType<InteractionMenu>(FindObjectsInactive.Include);
        taskController = FindAnyObjectByType<TaskController>(FindObjectsInactive.Include);
        itemHUD = FindAnyObjectByType<ItemHUD>(FindObjectsInactive.Include);
    }

    public void HandleItem(ItemDetermine itemObj)
    {
        bool itemClicked = itemObj.ChosenItem.Display();

        if (itemClicked && itemObj.ChosenItem.isPartOfTask == false)
        {
            interactionMenu.item = (Item)itemObj.ChosenItem;
            interactionMenu.gameObject.SetActive(true);
            interactionMenu.DisplayInfo((Item)itemObj.ChosenItem);
        }
        else if(itemClicked && itemObj.ChosenItem.isPartOfTask == true)
        {
            switch (itemObj.ChosenItem.Name)
            {
                case "Empty Box":

                    if(itemObj.BoxSortItem == player.curItem)
                    {
                        for (int i = 0; i < taskController.emptyBoxItems.Length; i++)
                        {
                            if (taskController.emptyBoxItems[i] == itemObj.BoxSortItem)
                            {
                                taskController.emptyBoxItems[i] = null;
                                break;
                            }
                        }

                        player.curItem = null;
                        itemHUD.SetImage(null);
                        Destroy(itemObj.gameObject);
                    }

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
        }
    }
}
