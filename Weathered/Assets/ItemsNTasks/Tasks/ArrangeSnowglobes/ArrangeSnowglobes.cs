using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Task;
//using static UnityEditor.PlayerSettings;

public class ArrangeSnowglobes : Task
{
    public List<Snowglobe> snowGlobes = new List<Snowglobe>();
    [SerializeField] List<SnowglobeObj> snowGlobeObjs = new List<SnowglobeObj>();
    public List<GameObject> shelfSnowGlobes = new List<GameObject>();
    [SerializeField] List<SnowglobeObj> snowGlobeObjsNotShelf = new List<SnowglobeObj>();
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

    private void Start()
    {
        StartCoroutine(SetSnowglobes());
    }

    IEnumerator SetSnowglobes()
    {
        yield return new WaitForSeconds(1f);
    }

    public override void InstanceTask()
    {
        base.InstanceTask();
        player = FindAnyObjectByType<PlayerController>();
        currentSGState = SGState.OutOfShelf;

        //randomize snowglobe postition
        RandomizePositions();
    }

    public void ShelfClicked()
    {
        shelfObj.SetActive(true);
        snowglobesUI.SetActive(true);
        snowglobesUnder.SetActive(false);
        UIController.UIControl.inputHandler.SetActive(false);
        currentSGState = SGState.InShelf;
        player.moveBlockers["Menu"] = true;

        foreach(Transform obj in slotParent.transform)
        {
            if(obj.GetComponent<SnowglobeObj>().sgItem.currentSGType != Snowglobe.sgType.Dehydration && obj.GetComponent<SnowglobeObj>().sgItem.currentSGType != Snowglobe.sgType.Glory)
            {
                var objImg = obj.GetComponent<SnowglobeObj>().gameObject.GetComponent<Image>();
                objImg.sprite = obj.GetComponent<SnowglobeObj>().sgItem.sgImg.sprite;
            }
        }
    }

    void RandomizePositions()
    {
        for (int i = 0; i < snowGlobeObjs.Count; i++)
        {
            var slotObj = Instantiate(slotOriginal, slotParent.transform);
            var objImg = slotObj.gameObject.GetComponent<Image>();

            var chosenNum = Random.Range(0, snowGlobes.Count);
            int showNum = chosenNum;


            if (snowGlobes[chosenNum].chosen == false)
            {
                if (snowGlobes[chosenNum].currentSGType != Snowglobe.sgType.Dehydration && snowGlobes[chosenNum].currentSGType != Snowglobe.sgType.Glory)
                    slotObj.sgItem = snowGlobes[chosenNum]; 
            }
            else
            {
                do
                {
                    chosenNum = Random.Range(0, snowGlobes.Count);
                } while (snowGlobes[chosenNum].chosen == true);

                if (snowGlobes[chosenNum].currentSGType != Snowglobe.sgType.Dehydration && snowGlobes[chosenNum].currentSGType != Snowglobe.sgType.Glory)
                    slotObj.sgItem = snowGlobes[chosenNum]; 
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
        UpdateShelf();
    }

    public void UpdateShelf()
    {
        for (int i = 0; i < slotParent.GetComponentsInChildren<SnowglobeObj>().Length; i++)
        {
            if (slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem.currentSGType != Snowglobe.sgType.Placeholder)
                shelfSnowGlobes[i].GetComponentInChildren<SpriteRenderer>().sprite = slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem.sgImg.sprite;
            else
            {
                if(slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem.chosen != true)
                    shelfSnowGlobes[i].GetComponentInChildren<SpriteRenderer>().sprite = null;
                else
                    shelfSnowGlobes[i].GetComponentInChildren<SpriteRenderer>().sprite = slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem.sgImg.sprite;
            }
        }
    }


    public void CheckForFinished()
    {
        bool inOrder = true;
        for(int i = 0; i < slotParent.GetComponentsInChildren<SnowglobeObj>().Length;  i++)
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
            shelfSnowGlobes[i].GetComponentInChildren<SnowglobeObj>().sgItem = snowGlobes[i];

            slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem = snowGlobes[i];

            shelfSnowGlobes[i].GetComponentInChildren<SpriteRenderer>().sprite = snowGlobes[i].sgImg.sprite;
            slotParent.GetComponentsInChildren<SnowglobeObj>()[i].sgItem.sgImg.sprite = snowGlobes[i].sgImg.sprite;
            slotParent.GetComponentsInChildren<SnowglobeObj>()[i].GetComponent<Image>().sprite = snowGlobes[i].sgImg.sprite;
            slotParent.GetComponentsInChildren<SnowglobeObj>()[i].GetComponent<Image>().color = Color.white;

            snowGlobes[0].sgButtons.DisableButton();
        }

        //shelfObj.SetActive(false);

        foreach (SnowglobeObj sg in snowGlobeObjsNotShelf)
        {
            sg.gameObject.SetActive(false);
        }
        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
