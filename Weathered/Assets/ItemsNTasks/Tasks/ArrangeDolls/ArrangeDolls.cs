using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeDolls : Task
{
    [SerializeField] List<Item> validDolls = new List<Item>();
    [SerializeField] public List<ArrangeDollPlaces> allPlaces = new List<ArrangeDollPlaces>();
    [SerializeField] GameObject sittingBear;
    [SerializeField] GameObject sittingBenni;
    [SerializeField] GameObject sittingClemmy;
    [SerializeField] GameObject sittingSaint;
    [SerializeField] GameObject sittingSally;
    [SerializeField] GameObject headPopDeath;
    FixDolls fixTask;

    [SerializeField] GameObject[] dollsToDisable;

    public override void InstanceTask()
    {
        base.InstanceTask();
        StartCoroutine(InsertDolls());
    }

    IEnumerator InsertDolls()
    {
        yield return new WaitForSeconds(1);
        validDolls.Clear();
        validDolls.Add(FindAnyObjectByType<Benni>());
        validDolls.Add(FindAnyObjectByType<Clemmy>());
        validDolls.Add(FindAnyObjectByType<MrBear>());
        validDolls.Add(FindAnyObjectByType<SaintBearnard>());
        validDolls.Add(FindAnyObjectByType<SallyMae>());
    }

    public void DollClicked(ArrangeDollsDoll dollClicked)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        ItemController.AddItemToHand(dollClicked.GetDollItem());
        Destroy(dollClicked.gameObject);
    }
    public void PlaceClicked(ArrangeDollPlaces placeClicked)
    {
        if (currentState == taskState.Completed)
        {
            if (fixTask == null)
            {
                fixTask = FindFirstObjectByType<FixDolls>();
            }

            fixTask.ClickedDoll(placeClicked.currentDoll, placeClicked);
        }
        else
        {
            if (placeClicked.currentDoll != null)
            {
                placeClicked.RetrieveDoll();
            }
            else if (validDolls.Contains(ItemController.itemInHand))
            {
                if (currentState == taskState.Available)
                {
                    OnInProgress();
                }
                Item tempDoll = ItemController.itemInHand;
                placeClicked.SetDoll(tempDoll);
                GameObject tempObject;
                switch (validDolls.IndexOf(tempDoll))
                {
                    case 0:
                        tempObject = Instantiate(sittingBenni, placeClicked.dollSitPosition);
                        break;
                    case 1:
                        tempObject = Instantiate(sittingClemmy, placeClicked.dollSitPosition);
                        break;
                    case 2:
                        tempObject = Instantiate(sittingBear, placeClicked.dollSitPosition);
                        break;
                    case 3:
                        tempObject = Instantiate(sittingSaint, placeClicked.dollSitPosition);
                        break;
                    default:
                        tempObject = Instantiate(sittingSally, placeClicked.dollSitPosition);
                        break;
                }
                placeClicked.satDoll = tempObject.GetComponent<SittingDoll>();
                SitDoll(tempObject, placeClicked.isInFront, placeClicked.facingNum);
                CheckProgress();
                if (!placeClicked.isCorrect)
                {
                    OnBadAction();
                }
                UpdateDolls();
            }
        }
    }
    void SitDoll(GameObject sitDollObject, bool isInFront, int facingNum)
    {
        foreach (SpriteRenderer childSR in sitDollObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (isInFront)
            {
                childSR.sortingLayerName = "InFront";
            }
            else
            {
                childSR.sortingLayerName = "Behind";
            }
        }
    }
    void CheckProgress()
    {
        bool isComplete = true;
        foreach (ArrangeDollPlaces place in allPlaces)
        {
            if (!place.isCorrect)
            {
                isComplete = false;
                break;
            }
        }
        if (isComplete)
        {
            OnCompleted();
        }
    }

    void UpdateDolls()
    {
        foreach (ArrangeDollPlaces place in allPlaces)
        {
            if (place.satDoll != null)
            {
                if (timesFailed >= 2)
                {
                    place.satDoll.isEvil = true;
                }
                place.satDoll.PoseDoll(place.facingNum);
            }
        }
    }

    public override void OnFailed()
    {
        base.OnFailed();
        Debug.Log("Mazarine lost her head trying to put the dolls in their rightful places.");
        GameManager.StartDeath(headPopDeath, 5f, true);
        foreach (ArrangeDollPlaces place in allPlaces)
        {
            if (place.satDoll != null && place.satDoll.hasEvil)
            {
                foreach (Transform childTransform in place.dollSitPosition)
                {
                    Destroy(childTransform.gameObject);
                }
            }
        }
        Destroy(FindFirstObjectByType<MrBear>().currentDroppedObject);
        Destroy(FindFirstObjectByType<SallyMae>().currentDroppedObject);
        Destroy(FindFirstObjectByType<SaintBearnard>().currentDroppedObject);
    }
    public override void LoadFinishedTask()
    {
        for (int i = 0; i < allPlaces.Count; i++)
        {
            for (int j = 0; j < allPlaces[i].dollSitPosition.GetComponentsInChildren<SittingDoll>().Length; j++)
            {
                Destroy(allPlaces[i].dollSitPosition.GetComponentsInChildren<SittingDoll>()[j].gameObject);
            }

            switch (i)
            {
                case 0:
                    var tempObject = Instantiate(sittingClemmy, allPlaces[i].dollSitPosition);
                    allPlaces[i].satDoll = tempObject.GetComponent<SittingDoll>();
                    SitDoll(tempObject, allPlaces[i].isInFront, allPlaces[i].facingNum);
                    break;
                case 1:
                    var tempObject1 = Instantiate(sittingSaint, allPlaces[i].dollSitPosition);
                    allPlaces[i].satDoll = tempObject1.GetComponent<SittingDoll>();
                    SitDoll(tempObject1, allPlaces[i].isInFront, allPlaces[i].facingNum);
                    break;
                case 2:
                    var tempObject2 = Instantiate(sittingSally, allPlaces[i].dollSitPosition);
                    allPlaces[i].satDoll = tempObject2.GetComponent<SittingDoll>();
                    SitDoll(tempObject2, allPlaces[i].isInFront, allPlaces[i].facingNum);
                    break;
                case 3:
                    var tempObject3 = Instantiate(sittingBenni, allPlaces[i].dollSitPosition);
                    allPlaces[i].satDoll = tempObject3.GetComponent<SittingDoll>();
                    SitDoll(tempObject3, allPlaces[i].isInFront, allPlaces[i].facingNum);
                    break;
                case 4:
                    var tempObject4 = Instantiate(sittingBear, allPlaces[i].dollSitPosition);
                    allPlaces[i].satDoll = tempObject4.GetComponent<SittingDoll>();
                    SitDoll(tempObject4, allPlaces[i].isInFront, allPlaces[i].facingNum);
                    break;
                default:
                    var tempObject5 = Instantiate(sittingClemmy, allPlaces[i].dollSitPosition);
                    allPlaces[i].satDoll = tempObject5.GetComponent<SittingDoll>();
                    SitDoll(tempObject5, allPlaces[i].isInFront, allPlaces[i].facingNum);
                    break;
            }
            allPlaces[i].isCorrect = true;
        }

        for (int i = 0; i < dollsToDisable.Length; i++)
        {
            dollsToDisable[i].SetActive(false);
        }

        UpdateDolls();
        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
