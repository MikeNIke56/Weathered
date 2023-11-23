using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Task;
using static UnityEditor.PlayerSettings;

public class ArrangeSnowglobes : Task
{
    [SerializeField] List<Snowglobe> snowGlobes = new List<Snowglobe>();
    [SerializeField] List<SnowglobeObj> snowGlobeObjs = new List<SnowglobeObj>();

    [SerializeField] GameObject shelfObj;
    public SnowglobeObj slotOriginal;
    public GameObject slotParent;

    public enum SGState { OutOfShelf, InShelf, InspectingSG }
    public SGState currentSGState;

    public bool isSwitching = false;

    public override void InstanceTask()
    {
        base.InstanceTask();
        currentSGState = SGState.OutOfShelf;

        //randomize snowglobe postition
        RandomizePositions();
    }

    public void ShelfClicked()
    {
        shelfObj.SetActive(true);
        currentSGState = SGState.InShelf;

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
                objImg.sprite = null;
        }
    }
}
