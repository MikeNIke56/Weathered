using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject loadScreen;
    [SerializeField] GameObject raccoon;
    bool activated = false;
    public void ContinueGame()
    {
        //will load saved data from the last time the player saved
    }

    public IEnumerator NewGame()
    {
        //opens the scene that will start a new game
        //right now im using this for testing stuff

        AsyncOperation operation = SceneManager.LoadSceneAsync("TestPlayer");
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            if(activated == false)
            {
                loadScreen.SetActive(true);
                raccoon.SetActive(true);
                activated = true;
            }
            yield return null;
        }

        //loadScreen.SetActive(true);
        //raccoon.SetActive(true);
        //SceneManager.LoadScene("TestPlayer");
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
        Application.Quit();
    }
}
