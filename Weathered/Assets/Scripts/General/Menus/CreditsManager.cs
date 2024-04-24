using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public int sceneIndex;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
