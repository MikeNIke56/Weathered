using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    [SerializeField] GameObject loadScreenObj;
    PlayerController player;
    public static ReloadScene i { get; private set; }
    private void Awake()
    {
        i = this;
    }

    public void AttachPlayerToReload(PlayerController player)
    {
        this.player = player;
    }

    public void ReloadSelectedScene(int slot)
    {
        UIController.UIControl.CloseSaveUI();
        UIController.UIControl.CloseBaseUI();
        UIController.UIControl.ToggleInputHandler(true);
        if (ItemController.itemInHand != null)
            ItemController.itemInHand.OnDropped();

        loadScreenObj.SetActive(true);
        player.moveBlockers["CutScene"] = true;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        StartCoroutine(AfterLoad(slot));
    }

    IEnumerator AfterLoad(int slot)
    {
        yield return new WaitForSeconds(2.5f);
        loadScreenObj.SetActive(false);
        player.moveBlockers["CutScene"] = false;
        UIController.UIControl.ToggleInputHandler(false);

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
