using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Task;
//using static UnityEditor.PlayerSettings;

public class ArrangeSnowglobes : Task
{
    public List<Snowglobe> snowGlobes;
    public List<GameObject> notShelfSnowGlobes;
    public List<GameObject> shelfSnowGlobeObjs;
    public Snowglobe placeHolderSG;

    public GameObject shelfObj;
    public GameObject snowglobesUI;
    public GameObject snowglobesUnder;
    public GameObject quitButton;

    public SnowglobeObj slotOriginal;
    public GameObject slotParent;
    public GameObject snowGlobesObjUnder;

    public enum SGState { OutOfShelf, InShelf, InspectingSG }
    public SGState currentSGState;

    public bool isSwitching = false;
    bool updated = false;

    public Snowglobe tempSG;
    public Image tempImg;

    PlayerController player;
    int sg6, sg7;

    UIController uiController;

    public override void InstanceTask()
    {
        base.InstanceTask();

        player = FindAnyObjectByType<PlayerController>();
        uiController = UIController.UIControl;
        currentSGState = SGState.OutOfShelf;

        SetSnowglobes();
    }

    void SetSnowglobes()
    {
        foreach (Transform obj in slotParent.transform)
            Destroy(obj.gameObject);

        sg6 = LayerMask.NameToLayer("sg6");
        sg7 = LayerMask.NameToLayer("sg7");

        var snowGlobesAry = FindAnyObjectByType<SnowGlobesAry>().GetComponentsInChildren<Snowglobe>();
        var snowGlobesObjAry = FindObjectsByType<SnowglobeObj>(FindObjectsSortMode.None);
        List<string> tempObjsNames = new List<string>();

        for (int i = 0; i < FindObjectsByType<Snowglobe>(FindObjectsSortMode.None).Length; i++)
            FindObjectsByType<Snowglobe>(FindObjectsSortMode.None)[i].SetGlobes();

        for (int i = 0; i < snowGlobesAry.Length; i++)
        {
            if (snowGlobesAry[i].gameObject.tag == "ShelfSG")
            {
                snowGlobes.Add(snowGlobesAry[i]);
                snowGlobesAry[i].snowglobes = this;
                snowGlobesAry[i].SetGlobes();
            }
            else if (snowGlobesAry[i].gameObject.tag == "placeholder")
                placeHolderSG = snowGlobesAry[i];
        }
        for (int i = 0; i < snowGlobesObjAry.Length; i++)
        {
            if (snowGlobesObjAry[i].gameObject.tag == "Untagged")
                tempObjsNames.Add(snowGlobesObjAry[i].name);
        }
        tempObjsNames.Sort();
        foreach (var name in tempObjsNames)
        {
            foreach (var obj in snowGlobesObjAry)
            {
                if(obj.name == name)
                    shelfSnowGlobeObjs.Add(obj.gameObject);
            }
        }

        foreach (var sg in FindObjectsByType<NotShelfGlobes>(FindObjectsSortMode.None))
        {
            notShelfSnowGlobes.Add(sg.gameObject);

            if (sg.gameObject.layer == sg6)
                sg.gameObject.GetComponent<SnowglobeObj>().sgItem = FindFirstObjectByType<NSGHolder>().sg6SG;
            else if (sg.gameObject.layer == sg7)
                sg.gameObject.GetComponent<SnowglobeObj>().sgItem = FindFirstObjectByType<NSGHolder>().sg7SG;
        }

        updated = false;
        if (GameManager.GM.hasReloaded == false)
        {
            GameManager.GM.SetGlobeData(placeHolderSG, shelfObj, snowglobesUI, snowglobesUnder,
                quitButton, slotOriginal, slotParent, snowGlobesObjUnder, snowGlobes);

            GameManager.GM.ClearGlobeItem();
            for (int i = 0; i < snowGlobesObjAry.Length; i++)
                GameManager.GM.AddItem(snowGlobesObjAry[i].sgItem);
        }
        else
        {
            placeHolderSG = GameManager.GM.GetPlaceholder();
            shelfObj = GameManager.GM.GetShelfObj();
            slotParent = GameManager.GM.GetSlotParent();
            slotOriginal = GameManager.GM.GetSlotOriginal();
            snowglobesUnder = GameManager.GM.GetSGUnder();
            snowGlobesObjUnder = GameManager.GM.GetSGObjUnder();
            quitButton = GameManager.GM.GetQuitButton();
            snowglobesUI = GameManager.GM.GetSGUI();
            snowGlobes = GameManager.GM.GetSnowglobes();

            for (int i = 0; i < snowGlobesObjAry.Length; i++)
            {
                snowGlobesObjAry[i].sgItem = GameManager.GM.GetItems()[i];
            }
        }

        updated = true;
        RandomizePositions();
    }

    public void ShelfClicked()
    {
        shelfObj.SetActive(true);
        snowglobesUI.SetActive(true);
        snowglobesUnder.SetActive(false);
        uiController.ToggleInputHandler(true);
        uiController.CloseBaseUI();
        currentSGState = SGState.InShelf;
        player.moveBlockers["Menu"] = true;

        foreach (Transform obj in slotParent.transform)
        {
            if (obj.GetComponent<SnowglobeObj>().sgItem.currentSGType != Snowglobe.sgType.Dehydration && obj.GetComponent<SnowglobeObj>().sgItem.currentSGType != Snowglobe.sgType.Glory)
            {
                var objImg = obj.GetComponent<SnowglobeObj>().gameObject.GetComponent<Image>();
                objImg.sprite = obj.GetComponent<SnowglobeObj>().sgItem.sgImg.sprite;
            }
        }
    }

    void RandomizePositions()
    {
        if (GameManager.GM.hasReloaded == true)
            snowGlobes = GameManager.GM.GetSnowglobes();

        if (updated == true)
        {
            for (int i = 0; i < snowGlobes.Count; i++)
            {
                var slotObj = Instantiate(slotOriginal, slotParent.transform);
                var objImg = slotObj.gameObject.GetComponent<Image>();

                var chosenNum = Random.Range(0, snowGlobes.Count);

                if (snowGlobes[chosenNum].chosen == false)
                {
                    if (snowGlobes[chosenNum].currentSGType != Snowglobe.sgType.Dehydration && snowGlobes[chosenNum].currentSGType != Snowglobe.sgType.Glory)
                        slotObj.sgItem = snowGlobes[chosenNum];

                }
                else
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (snowGlobes[j].chosen == false)
                        {
                            chosenNum = j;

                            if (snowGlobes[chosenNum].currentSGType != Snowglobe.sgType.Dehydration && snowGlobes[chosenNum].currentSGType != Snowglobe.sgType.Glory)
                            {
                                slotObj.sgItem = snowGlobes[chosenNum];
                                break;
                            }
                        }
                    }
                }

                snowGlobes[chosenNum].chosen = true;

                if (slotObj.sgItem != null)
                {
                    objImg.sprite = slotObj.sgItem.sgImg.sprite;
                    slotObj.sgItem.included = true;
                }
                else
                {
                    slotObj.sgItem = placeHolderSG;
                    objImg.sprite = placeHolderSG.sgImg.sprite;
                    slotObj.GetComponent<Image>().color = Color.black;
                }
            }
            Debug.Log("waited");
            UpdateShelf();
        } 
        else
        {
            Debug.Log("did not wait");
        }
    }

    public void UpdateShelf()
    {
        if (GameManager.GM.hasReloaded == true)
            snowGlobes = GameManager.GM.GetSnowglobes();

        List<SnowglobeObj> slots = new List<SnowglobeObj>();

        for (int i = 0; i < slotParent.GetComponentsInChildren<SnowglobeObj>().Length; i++)
        {
            if (slotParent.GetComponentsInChildren<SnowglobeObj>()[i] != null && i < 8)
                slots.Add(slotParent.GetComponentsInChildren<SnowglobeObj>()[i]);
            else if (i >= 8)
                Destroy(slotParent.GetComponentsInChildren<SnowglobeObj>()[i].gameObject);
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].sgItem.currentSGType != Snowglobe.sgType.Placeholder)
            {
                shelfSnowGlobeObjs[i].GetComponentInChildren<SpriteRenderer>().sprite = slots[i].sgItem.sgImg.sprite;
            }
            else
            {
                if (slots[i].sgItem.chosen != true)
                    shelfSnowGlobeObjs[i].GetComponentInChildren<SpriteRenderer>().sprite = null;
                else
                    shelfSnowGlobeObjs[i].GetComponentInChildren<SpriteRenderer>().sprite = slots[i].sgItem.sgImg.sprite;
            }
        }


    }

    public void CheckForFinished()
    {
        bool inOrder = true;
        for (int i = 0; i < slotParent.GetComponentsInChildren<SnowglobeObj>().Length; i++)
        {
            if (slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem != snowGlobes[i])
                inOrder = false;
        }

        if (inOrder == true)
        {
            OnCompleted();
            snowGlobes[0].sgButtons.DisableButton();
            Debug.Log("done");
        }
    }

    public override void LoadFinishedTask()
    {
        for (int i = 0; i < snowGlobes.Count; i++)
        {
            //if (shelfObj.activeInHierarchy == false)
            // shelfObj.SetActive(true);


            snowGlobes[i].isShowing = true;
            snowGlobes[i].included = true;
            shelfSnowGlobeObjs[i].GetComponentInChildren<SnowglobeObj>().sgItem = snowGlobes[i];

            slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem = snowGlobes[i];

            shelfSnowGlobeObjs[i].GetComponentInChildren<SpriteRenderer>().sprite = snowGlobes[i].sgImg.sprite;
            slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem.sgImg.sprite = snowGlobes[i].sgImg.sprite;
            slotParent.GetComponentsInChildren<SnowglobeObj>()[i].GetComponent<Image>().sprite = snowGlobes[i].sgImg.sprite;
            slotParent.GetComponentsInChildren<SnowglobeObj>()[i].GetComponent<Image>().color = Color.white;

            snowGlobes[0].sgButtons.DisableButton();
        }

        //shelfObj.SetActive(false);

        foreach (GameObject sg in notShelfSnowGlobes)
        {
            sg.SetActive(false);
        }
        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
