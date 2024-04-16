using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSceneHandle : MonoBehaviour
{
    public static MainGameSceneHandle i { get; private set; }

    private void Awake()
    {
        i = this;
    }
    void Start()
    {
        AfterLoad(FindAnyObjectByType<ReloadScene>().slot);
        Progression.Prog.HandleReloadedAssets();
        Time.timeScale = 1;
    }

    public void AfterLoad(int slot)
    {
        SavingSystem.i.Load($"SaveSlot" + slot.ToString());
    }
}
