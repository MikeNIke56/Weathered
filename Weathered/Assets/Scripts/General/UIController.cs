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

    [SerializeField] GameObject snowglobesUI;
    ArrangeSnowglobes snowglobes;
    public GameObject inputHandler;

    PlayerController player;
    
    public bool isTasksMenuOpen = false;
    void Start()
    {
        if (UIControl == null)
        {
            UIControl = FindFirstObjectByType<UIController>();
            snowglobes = FindAnyObjectByType<ArrangeSnowglobes>();
            player = FindFirstObjectByType<PlayerController>();
        }
    }

    public void OpenInteractionMenu()
    {
        baseGameUI.SetActive(false);
        tasksMenu.SetActive(false);
        investigateMenu.SetActive(true);
        inputHandler.SetActive(false);
        player.lockMovement = true;
    }

    public void CloseInteractionMenu()
    {
        investigateMenu.SetActive(false);
        snowglobesUI.SetActive(false);
        inputHandler.SetActive(true);
        snowglobes.currentSGState = ArrangeSnowglobes.SGState.OutOfShelf;
        if (isTasksMenuOpen)
        {
            tasksMenu.SetActive(true);
        }
        baseGameUI.SetActive(true);
        player.lockMovement = false;
    }

    public void OpenTasksMenu()
    {
        isTasksMenuOpen = true;
        tasksMenu.SetActive(true);
        inputHandler.SetActive(false);
        player.state = PlayerController.GameState.Menu;
    }
    public void CloseTasksMenu()
    {
        isTasksMenuOpen = false;
        tasksMenu.SetActive(false);
        inputHandler.SetActive(true);
        player.state = PlayerController.GameState.FreeRoam;
    }
}
