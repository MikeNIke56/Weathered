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
    [SerializeField] List<GameObject> shelfSnowGlobeObjs;
    public Snowglobe placeHolderSG;

    [SerializeField] GameObject shelfObj;
    [SerializeField] GameObject snowglobesUI;
    [SerializeField] GameObject snowglobesUnder;
    public GameObject quitButton;

    public SnowglobeObj slotOriginal;
    public GameObject slotParent;
    public GameObject snowGlobesObjUnder;

    public enum SGState { OutOfShelf, InShelf, InspectingSG }
    public SGState currentSGState;

    public bool isSwitching = false;

    public Snowglobe tempSG;
    public Image tempImg;

    PlayerController player;
    int sg6, sg7;

    public override void InstanceTask()
    {
        base.InstanceTask();
        player = FindAnyObjectByType<PlayerController>();
        currentSGState = SGState.OutOfShelf;

        SetSnowglobes();
    }

    void SetSnowglobes()
    {
        sg6 = LayerMask.NameToLayer("sg6");
        sg7 = LayerMask.NameToLayer("sg7");

        var snowGlobesAry = FindAnyObjectByType<SnowGlobesAry>().GetComponentsInChildren<Snowglobe>();
        var snowGlobesObjAry = FindObjectsByType<SnowglobeObj>(FindObjectsSortMode.None);
        List<string> tempObjsNames = new List<string>();

        for (int i = 0; i < snowGlobesAry.Length; i++)
        {
            if (snowGlobesAry[i].gameObject.tag == "ShelfSG")
            {
                snowGlobes.Add(snowGlobesAry[i]);
                snowGlobesAry[i].snowglobes = this;
            }
            else if (snowGlobesAry[i].gameObject.tag == "placeholder")
                placeHolderSG = snowGlobesAry[i];
        }
        for (int i = 0; i < snowGlobesObjAry.Length; i++)
        {
            if (snowGlobesObjAry[i].gameObject.tag == "Untagged")
                tempObjsNames.Add(snowGlobesObjAry[i].name);

            for (int j = 0; j < snowGlobes.Count; j++)
            {
                if (snowGlobes[j].currentSGType == Snowglobe.sgType.Glory)
                    snowGlobesObjAry[i].gameObject.GetComponent<SnowglobeObj>().sgItem = snowGlobes[j];
            }
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

        shelfObj = FindAnyObjectByType<SnowParent>(FindObjectsInactive.Include).gameObject;
        slotParent = FindAnyObjectByType<SlotParent>(FindObjectsInactive.Include).gameObject;
        snowglobesUI = FindAnyObjectByType<SlotParent>(FindObjectsInactive.Include).gameObject;
        snowglobesUnder = FindAnyObjectByType<SnowglobesButtons>(FindObjectsInactive.Include).gameObject;
        snowGlobesObjUnder = FindAnyObjectByType<SnowglobesButtons>(FindObjectsInactive.Include).gameObject;
        quitButton = FindAnyObjectByType<QuitButton>(FindObjectsInactive.Include).gameObject;

        RandomizePositions();
    }

    public void ShelfClicked()
    {
        shelfObj.SetActive(true);
        snowglobesUI.SetActive(true);
        snowglobesUnder.SetActive(false);
        UIController.UIControl.inputHandler.SetActive(false);
        UIController.UIControl.CloseBaseUI();
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
        for (int i = 0; i < snowGlobes.Count; i++)
        {
            var slotObj = Instantiate(slotOriginal, slotParent.transform);
            var objImg = slotObj.gameObject.GetComponent<Image>();

            //List<int> numsLeft = new List<int>(8) { 0, 1, 2, 3, 4, 5, 6, 7 };
            //var chosenNum = Random.Range(0, numsLeft.Count);
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
            //numsLeft.RemoveAt(chosenNum);

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
        UpdateShelf();
    }



    public void UpdateShelf()
    {
        for (int i = 0; i < slotParent.GetComponentsInChildren<SnowglobeObj>().Length; i++)
        {
            if (slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem.currentSGType != Snowglobe.sgType.Placeholder)
            {
                shelfSnowGlobeObjs[i].GetComponentInChildren<SpriteRenderer>().sprite = slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem.sgImg.sprite;
            }
            else
            {
                if (slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem.chosen != true)
                    shelfSnowGlobeObjs[i].GetComponentInChildren<SpriteRenderer>().sprite = null;
                else
                    shelfSnowGlobeObjs[i].GetComponentInChildren<SpriteRenderer>().sprite = slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem.sgImg.sprite;
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
