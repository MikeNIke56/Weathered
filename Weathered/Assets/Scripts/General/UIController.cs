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
    [SerializeField] GameObject deathScreen;

    [SerializeField] GameObject auntVoicemailScreen;

    [SerializeField] GameObject snowglobesUI;
    ArrangeSnowglobes snowglobes;
    ShelfScrolling shelf;
    public GameObject inputHandler;

    PlayerController player;
    [SerializeField] AudioSource openNotes;
    [SerializeField] public AudioSource closeNotes;

    public bool isTasksMenuOpen = false;
    void Start()
    {
        if (UIControl == null)
        {
            UIControl = FindFirstObjectByType<UIController>();
            snowglobes = FindAnyObjectByType<ArrangeSnowglobes>();
            player = FindFirstObjectByType<PlayerController>();
            shelf = FindFirstObjectByType<ShelfScrolling>(FindObjectsInactive.Include);
        }
    }

    public void OpenInteractionMenu()
    {
        baseGameUI.SetActive(false);
        tasksMenu.SetActive(false);
        investigateMenu.SetActive(true);
        inputHandler.SetActive(false);
        player.moveBlockers["Menu"] = true;
    }

    public void CloseInteractionMenu()
    {
        investigateMenu.SetActive(false);
        snowglobesUI.SetActive(false);
        shelf.Deactivate();
        inputHandler.SetActive(true);
        snowglobes.currentSGState = ArrangeSnowglobes.SGState.OutOfShelf;
        if (isTasksMenuOpen)
        {
            tasksMenu.SetActive(true);
        }
        baseGameUI.SetActive(true);
        player.moveBlockers["Menu"] = false;
    }

    public void OpenTasksMenu()
    {
        isTasksMenuOpen = true;
        tasksMenu.SetActive(true);
        inputHandler.SetActive(false);
        player.state = PlayerController.GameState.Menu;
        player.moveBlockers["Menu"] = true;
        openNotes.Play();
    }
    public void CloseTasksMenu()
    {
        isTasksMenuOpen = false;
        tasksMenu.SetActive(false);
        inputHandler.SetActive(true);
        player.state = PlayerController.GameState.FreeRoam;
        player.moveBlockers["Menu"] = false;
    }
    public void DeathEventScreen()
    {
        isTasksMenuOpen = false;
        tasksMenu.SetActive(false);
        inputHandler.SetActive(false);
        snowglobesUI.SetActive(false);
        baseGameUI.SetActive(false);
        investigateMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }
    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void HandleTutorial(bool isOn)
    {
        if(isOn == true)
            inputHandler.SetActive(false);
        else
            inputHandler.SetActive(true);
    }

    public void OpenAuntVoicemail()
    {
        baseGameUI.SetActive(false);
        tasksMenu.SetActive(false);
        investigateMenu.SetActive(false);
        inputHandler.SetActive(false);
        auntVoicemailScreen.SetActive(true);
        player.moveBlockers["Menu"] = true;
    }
    public void CloseAuntVoicemail()
    {
        baseGameUI.SetActive(true);
        tasksMenu.SetActive(false);
        investigateMenu.SetActive(false);
        inputHandler.SetActive(true);
        auntVoicemailScreen.SetActive(false);
        player.moveBlockers["Menu"] = false;
    }
}
