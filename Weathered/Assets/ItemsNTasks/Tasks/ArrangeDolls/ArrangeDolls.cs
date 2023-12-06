using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeDolls : Task
{
    [SerializeField] List<Item> validDolls = new List<Item>();
    [SerializeField] List<ArrangeDollPlaces> allPlaces = new List<ArrangeDollPlaces>();
    [SerializeField] GameObject sittingBear;
    [SerializeField] GameObject sittingBenni;
    [SerializeField] GameObject sittingClemmy;
    [SerializeField] GameObject sittingSaint;
    [SerializeField] GameObject sittingSally;
    [SerializeField] GameObject headPopDeath;
    FixDolls fixTask;
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
        GameManager.StartDeath(headPopDeath,5f,true);
    }
}
