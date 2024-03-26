using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{
    public GameObject slotsUI;
    public GameObject buttonsUI;
    bool isSave;

    public GameObject[] slots;
    [SerializeField] MainMenuManager mainMenu;


    private void Start()
    {
        if (SavingSystem.i.GetPath("SaveSlot1") != null)
            slots[0].GetComponentInChildren<Text>().text = File.GetLastWriteTime(SavingSystem.i.GetPath("SaveSlot1")).ToString();
        if (SavingSystem.i.GetPath("SaveSlot2") != null)
            slots[1].GetComponentInChildren<Text>().text = File.GetLastWriteTime(SavingSystem.i.GetPath("SaveSlot2")).ToString();
        if (SavingSystem.i.GetPath("SaveSlot3") != null)
            slots[2].GetComponentInChildren<Text>().text = File.GetLastWriteTime(SavingSystem.i.GetPath("SaveSlot3")).ToString();
    }
    private void Update()
    {

    }

    public void ContinueSave()
    {
        slotsUI.SetActive(true);
        isSave = true;
        buttonsUI.SetActive(false);
    }
    public void ContinueLoad()
    {
        slotsUI.SetActive(true);
        isSave = false;
        buttonsUI.SetActive(false);
    }

    public void ChooseSlot(int slot)
    {
        if (isSave==true)
        {
            switch (slot)
            {
                case 1:
                    SavingSystem.i.Save("SaveSlot1");
                    slots[0].GetComponentInChildren<Text>().text = DateTime.Now.ToString();
                    break;
                case 2:
                    SavingSystem.i.Save("SaveSlot2");
                    slots[1].GetComponentInChildren<Text>().text = DateTime.Now.ToString();
                    break;
                case 3:
                    SavingSystem.i.Save("SaveSlot3");
                    slots[2].GetComponentInChildren<Text>().text = DateTime.Now.ToString();
                    break;
                default:
                    SavingSystem.i.Save("SaveSlot1");
                    slots[0].GetComponentInChildren<Text>().text = DateTime.Now.ToString();
                    break;
            }
        }
        else
        {
            switch (slot)
            {
                case 1:
                    ReloadScene.i.slot = slot;
                    ReloadScene.i.LoadSelectedFile();
                    break;
                case 2:
                    ReloadScene.i.slot = slot;
                    ReloadScene.i.LoadSelectedFile();
                    break;
                case 3:
                    ReloadScene.i.slot = slot;
                    ReloadScene.i.LoadSelectedFile();
                    break;
                default:
                    Debug.Log("failed load");
                    break;
            }
        }
    }
}
