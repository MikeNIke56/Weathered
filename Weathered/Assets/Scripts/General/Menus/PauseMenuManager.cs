using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }
    private void Update()
    {

    }

    public void Continue()
    {
        Time.timeScale = 1f;
        playerController.isPaused = false;
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }


}
