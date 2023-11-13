using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanFloor : Task
{
    [SerializeField] List<BrokenGlass> brokenGlass = new List<BrokenGlass>();
    [SerializeField] List<BloodSplatter> bloodSplatters = new List<BloodSplatter>();

    [SerializeField] Duster dusterItem;
    [SerializeField] Mop mopItem;

    bool clearedGlass = true;
    bool clearedSplatters = true;

    public override void InstanceTask()
    {
        base.InstanceTask();

        foreach (BrokenGlass glass in brokenGlass)
        {
            glass.gameObject.SetActive(true);
        }
        foreach (BloodSplatter splatters in bloodSplatters)
        {
            splatters.gameObject.SetActive(true);
        }
    }
    public void ClickedSpot(Interaction interaction)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        if(interaction is BrokenGlass)
        {
            if (ItemController.itemInHand == dusterItem)
            {
                interaction.gameObject.SetActive(false);
            }

            foreach (BrokenGlass glass in brokenGlass)
            {
                if (glass.gameObject.activeInHierarchy)
                {
                    clearedGlass = false;
                }
            }
        }
        else if (interaction is BloodSplatter)
        {
            if (ItemController.itemInHand == mopItem)
            {
                interaction.gameObject.SetActive(false);
            }

            foreach (BloodSplatter splatters in bloodSplatters)
            {
                if (splatters.gameObject.activeInHierarchy)
                {
                    clearedSplatters = false;
                }
            }
        }

        if (clearedGlass == true && clearedSplatters == true)
        {
            OnCompleted();
        }

    }
}
