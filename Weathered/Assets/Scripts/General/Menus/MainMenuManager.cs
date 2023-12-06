using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject loadScreen;
    [SerializeField] GameObject raccoon;
    public void ContinueGame()
    {
        //will load saved data from the last time the player saved
    }

    public void NewGame()
    {
        //opens the scene that will start a new game
        //right now im using this for testing stuff

        SceneManager.LoadScene("TestPlayer");
        loadScreen.SetActive(true);
        raccoon.SetActive(true);
    }
    public void LoadGame()
    {

    }

    public void OpenOptions()
    {

    }

    public void ExitGame()
    {
        //quits game
    }
}
