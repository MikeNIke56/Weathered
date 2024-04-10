using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] InputHandler inputHandler;
    [SerializeField] MainMenuManager mainMenu;
    public int slot;

    public static ReloadScene i { get; private set; }
    private void Awake()
    {
        i = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {

    }

    public void LoadSelectedFile()
    {
        StartCoroutine(mainMenu.StartNewGame());
    }
}
