using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanMirrors : Task
{
    [SerializeField] List<MirrorSmudges> smudges = new List<MirrorSmudges>();
    [SerializeField] MirrorCloth clothItem;

    bool clearedAllMirrors = false;


    public override void InstanceTask()
    {
        base.InstanceTask();

        foreach (MirrorSmudges smudge in smudges)
        {
            smudge.gameObject.SetActive(true);
        }
    }
    public void ClickedSmudge(Interaction interaction)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        if (interaction is MirrorSmudges)
        {
            if (ItemController.itemInHand == clothItem)
            {
                if (interaction.gameObject.GetComponent<MirrorSmudges>().cleanCount > 0)
                {
                    interaction.gameObject.GetComponent<MirrorSmudges>().cleanCount--;
                    if(interaction.gameObject.GetComponent<MirrorSmudges>().cleanCount <= 0)
                        interaction.gameObject.SetActive(false);
                }
                else
                    interaction.gameObject.SetActive(false);
            }

            clearedAllMirrors = true;
            foreach (MirrorSmudges smudge in smudges)
            {
                if (smudge.gameObject.activeInHierarchy)
                {
                    clearedAllMirrors = false;
                }
            }
        }

        if (clearedAllMirrors == true)
        {
            OnCompleted();
        }

    }
    public override void OnFailed()
    {
        //trigger death condition
        Debug.Log("player has died");
    }

    /*public override void LoadFinishedTask()
    {
        foreach (BloodSplatter splatters in bloodSplatters)
        {
            splatters.gameObject.SetActive(false);
        }
        foreach (BrokenGlass glass in brokenGlass)
        {
            glass.gameObject.SetActive(false);
        }
        closet.OnCompletedLoad();
        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }*/
}
