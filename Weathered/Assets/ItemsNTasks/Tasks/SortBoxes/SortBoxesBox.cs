using System;
using UnityEngine;

public class SortBoxesBox : Interaction
{
    [SerializeField] SortBoxes SBTask;
    [SerializeField] Sprite boxOpenSprite;
    public bool isOpened = false;
    public bool isDone = false;
    public AudioSource boxSound;

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
        boxSound.Play();
    }
    public void RemoveBox()
    {
        isDone = true;
        try
        {
            boxSound.Play();
            gameObject.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log("no box available");
            Debug.Log(e);
        }
    }
}
