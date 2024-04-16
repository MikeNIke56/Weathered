using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject loadScreen;
    [SerializeField] GameObject raccoon;
    bool activated = false;
    public GameObject gameFileSelectUI;
    public void ContinueGame()
    {
        //will load saved data from the last time the player saved
    }

    public IEnumerator StartNewGame()
    {
        //opens the scene that will start a new game

        //loading main scene in background
        UnityEngine.AsyncOperation operation = SceneManager.LoadSceneAsync("TestPlayer");
        operation.allowSceneActivation = false;

        //while scene is being loaded
        while(!operation.isDone)
        {
            if (activated == false)
            {
                //the loading screen will appear and the raccoon wave animation will play
                loadScreen.SetActive(true);
                raccoon.SetActive(true);
                activated = true;

                //tiny buffer between the loading screen stuff and the below logic
                yield return new WaitForSeconds(.5f);
            }

            //once loading of the main scene is done- loads it in
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void NewGame()
    {
        StartCoroutine(StartNewGame());
    }

    public void BackToMenu()
    {
        gameFileSelectUI.SetActive(false);
    }

    public IEnumerator LoadGame()
    {
        yield return null;
    }

    public void OpenOptions()
    {

    }

    public void ExitGame()
    {
        //quits game
        Application.Quit();
    }
}
