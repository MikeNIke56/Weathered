using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortBoxesBox : Interaction
{
    [SerializeField] SortBoxes SBTask;
    [SerializeField] Sprite boxOpenSprite;
    public bool isOpened = false;
    public bool isDone = false;
    public override void onClick()
    {
        if (SBTask == null)
        {
            SBTask = FindFirstObjectByType<SortBoxes>();
        }

        SBTask.BoxClicked(this);
    }
    public void OpenBox()
    {
        isOpened = true;
        GetComponent<SpriteRenderer>().sprite = boxOpenSprite;
    }
    public void RemoveBox()
    {
        isDone = true;
        gameObject.SetActive(false);
    }
}
