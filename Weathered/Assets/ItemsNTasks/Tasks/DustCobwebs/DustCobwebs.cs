using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCobwebs : Task
{
    [SerializeField] List<DustWebsWeb> allCobwebs = new List<DustWebsWeb>();
    [SerializeField] Duster dusterItem;

    public bool clearedCobweb = false;
    public bool cobwebsDone = false;

    public override void InstanceTask()
    {
        base.InstanceTask();

        foreach (DustWebsWeb singleWeb in allCobwebs)
        {
            singleWeb.gameObject.SetActive(true);
        }
    }
    public void ClickedCobweb(DustWebsWeb webClicked)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        if (ItemController.itemInHand == dusterItem)
        {
            webClicked.gameObject.SetActive(false);
            clearedCobweb = true;
            dusterItem.sweepSfx.Play();
        }

        bool isWebsCleaned = true;

        foreach (DustWebsWeb singleWeb in allCobwebs)
        {
            if (singleWeb.gameObject.activeInHierarchy)
            {
                isWebsCleaned = false;
            }
        }

        if (isWebsCleaned)
        {
            OnCompleted();
            cobwebsDone = true;
        }
    }
}
