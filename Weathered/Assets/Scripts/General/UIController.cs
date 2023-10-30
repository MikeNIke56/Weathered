using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    static public UIController UIControl;
    [SerializeField] GameObject baseGameUI;
    [SerializeField] GameObject investigateMenu;
    [SerializeField] GameObject tasksMenu;
    [SerializeField] GameObject pauseMenu;
    public bool isTasksMenuOpen = false;
    void Start()
    {
        if (UIControl == null)
        {
            UIControl = FindFirstObjectByType<UIController>();
        }
    }

    public void OpenInteractionMenu()
    {
        baseGameUI.SetActive(false);
        tasksMenu.SetActive(false);
        investigateMenu.SetActive(true);
    }

    public void CloseInteractionMenu()
    {
        investigateMenu.SetActive(false);
        if (isTasksMenuOpen)
        {
            tasksMenu.SetActive(true);
        }
        baseGameUI.SetActive(true);
    }

    public void OpenTasksMenu()
    {
        isTasksMenuOpen = true;
        tasksMenu.SetActive(true);
    }
    public void CloseTasksMenu()
    {
        isTasksMenuOpen = false;
        tasksMenu.SetActive(false);
    }
}
