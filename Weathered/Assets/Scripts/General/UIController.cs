using UnityEngine;
using static PlayerController;

public class UIController : MonoBehaviour
{
    static public UIController UIControl;
    [SerializeField] GameObject baseGameUI;
    [SerializeField] GameObject investigateMenu;
    [SerializeField] GameObject tasksMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject saveScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject auntVoicemailScreen;

    [SerializeField] GameObject snowglobesUI;
    ArrangeSnowglobes snowglobes;
    ShelfScrolling shelf;
    public GameObject inputHandler;

    PlayerController player;
    [SerializeField] AudioSource openNotes;
    [SerializeField] public AudioSource closeNotes;

    public bool isCamFree = true;

    public bool isTasksMenuOpen = false;
    void Start()
    {
        if (UIControl == null)
        {
            UIControl = FindFirstObjectByType<UIController>();
        }
    }

    public void SetValues()
    {
        snowglobes = FindAnyObjectByType<ArrangeSnowglobes>();
        player = FindFirstObjectByType<PlayerController>();
        shelf = FindFirstObjectByType<ShelfScrolling>(FindObjectsInactive.Include);
    }

    public void OpenBaseUI()
    {
        baseGameUI.SetActive(true);
    }

    public void CloseBaseUI()
    {
        baseGameUI.SetActive(false);
    }

    public void OpenInteractionMenu()
    {
        isCamFree = false;
        baseGameUI.SetActive(false);
        tasksMenu.SetActive(false);
        inputHandler.SetActive(false);
        investigateMenu.SetActive(true);
        player.moveBlockers["Menu"] = true;
        saveScreen.SetActive(false);
    }
    public void CloseInteractionMenu()
    {
        isCamFree = true;
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
        isCamFree = false;
        isTasksMenuOpen = true;
        tasksMenu.SetActive(true);
        inputHandler.SetActive(false);
        saveScreen.SetActive(false);
        player.state = PlayerController.GameState.Menu;
        player.moveBlockers["Menu"] = true;
        openNotes.Play();
    }
    public void CloseTasksMenu()
    {
        isCamFree = true;
        isTasksMenuOpen = false;
        tasksMenu.SetActive(false);
        inputHandler.SetActive(true);
        player.state = PlayerController.GameState.FreeRoam;
        player.moveBlockers["Menu"] = false;
    }
    public void OpenSaveUI()
    {
        isCamFree = false;
        saveScreen.SetActive(true);
        tasksMenu.SetActive(false);
        inputHandler.SetActive(false);
        player.state = PlayerController.GameState.Menu;
        player.moveBlockers["Menu"] = true;
        Time.timeScale = 0f;
    }
    public void CloseSaveUI()
    {
        isCamFree = true;
        saveScreen.SetActive(false);
        saveScreen.GetComponent<SaveMenu>().buttonsUI.SetActive(true);
        saveScreen.GetComponent<SaveMenu>().slotsUI.SetActive(false);
        tasksMenu.SetActive(false);
        inputHandler.SetActive(false);
        player.state = PlayerController.GameState.FreeRoam;
        player.moveBlockers["Menu"] = false;
        Time.timeScale = 1f;
    }
    public void DeathEventScreen()
    {
        isCamFree = false;
        isTasksMenuOpen = false;
        tasksMenu.SetActive(false);
        inputHandler.SetActive(false);
        snowglobesUI.SetActive(false);
        baseGameUI.SetActive(false);
        investigateMenu.SetActive(false);
        pauseMenu.SetActive(false);
        saveScreen.SetActive(false);
    }
    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void ToggleInputHandler(bool isOn)
    {
        if (isOn == true)
            inputHandler.SetActive(false);
        else
            inputHandler.SetActive(true);
    }

    public void OpenDialog()
    {
        baseGameUI.SetActive(false);
        tasksMenu.SetActive(false);
        investigateMenu.SetActive(false);
        inputHandler.SetActive(false);
        auntVoicemailScreen.SetActive(false);
        saveScreen.SetActive(false);
        player.moveBlockers["Menu"] = true;
    }
    public void CloseDialog()
    {
        baseGameUI.SetActive(true);
        tasksMenu.SetActive(false);
        investigateMenu.SetActive(false);
        inputHandler.SetActive(true);
        auntVoicemailScreen.SetActive(false);
        player.moveBlockers["Menu"] = false;
        saveScreen.SetActive(false);
    }

    public void OpenAuntVoicemail()
    {
        baseGameUI.SetActive(false);
        tasksMenu.SetActive(false);
        investigateMenu.SetActive(false);
        inputHandler.SetActive(false);
        auntVoicemailScreen.SetActive(true);
        saveScreen.SetActive(false);
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
        saveScreen.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        if (player.isPaused == false)
        {
            isCamFree = false;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            player.isPaused = true;
        }
    }
    void ClosePauseMenu()
    {
        if (player.isPaused == true)
        {
            isCamFree = true;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            player.isPaused = false;
        }
    }
    public void Back()
    {
        isCamFree = true;
        CloseInteractionMenu();
        CloseTasksMenu();
        CloseSaveUI();
        CloseAuntVoicemail();
        ClosePauseMenu();
        player.state = GameState.FreeRoam;
        snowglobes.currentSGState = ArrangeSnowglobes.SGState.OutOfShelf;
        player.moveBlockers["Menu"] = false;
    }
}
