using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSceneHandle : MonoBehaviour
{
    private void Awake()
    {
        //AfterLoad(ReloadScene.i.slot);
    }
    void Start()
    {
        AfterLoad(ReloadScene.i.slot);
        Progression.Prog.HandleReloadedAssets();
        Time.timeScale = 1;
    }

    void AfterLoad(int slot)
    {
        switch (slot)
        {
            case 1:
                SavingSystem.i.Load("SaveSlot1");
                break;
            case 2:
                SavingSystem.i.Load("SaveSlot2");
                break;
            case 3:
                SavingSystem.i.Load("SaveSlot3");
                break;
            default:
                Debug.Log("failed load");
                break;
        }
    }
}
