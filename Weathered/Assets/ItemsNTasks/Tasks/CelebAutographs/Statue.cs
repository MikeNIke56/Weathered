using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : Interaction
{
    CelebAutoGraphs autoGraphs;
    [SerializeField] GameObject completedStatue;
    [SerializeField] GameObject head;
    public override void onClick()
    {
        if (autoGraphs == null)
        {
            autoGraphs = FindFirstObjectByType<CelebAutoGraphs>();
        }
        autoGraphs.ClickedStatueObject(this);
    }

    public void CompleteStatue()
    {
        completedStatue.SetActive(true);
        this.gameObject.SetActive(false);
        autoGraphs.requirementsMet[2] = true;
        ItemController.ClearItemInHand();
        head.SetActive(false);
    }
}
