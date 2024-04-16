using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class SaveMenu : MonoBehaviour
{
    public GameObject slotsUI;
    public GameObject buttonsUI;
    bool isSave;

    public GameObject[] slots;
    [SerializeField] MainMenuManager mainMenu;


    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var fileNum = i + 1;
            var saveFileLoc = Path.Combine(Application.persistentDataPath, "SaveSlot" + fileNum.ToString());

            if (File.Exists(saveFileLoc))
            {
                var saveFile = SavingSystem.i.GetPath("SaveSlot" + fileNum.ToString());
                slots[i].GetComponentInChildren<Text>().text = File.GetLastWriteTime(saveFile).ToString();
            }
            else
                slots[i].GetComponentInChildren<Text>().text = "Empty...";
        }

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
            SavingSystem.i.Save($"SaveSlot" + slot.ToString());
            slots[slot-1].GetComponentInChildren<Text>().text = DateTime.Now.ToString();
        }
        else
        {
            ReloadScene.i.slot = slot;
            ReloadScene.i.LoadSelectedFile();
        }
    }
}
