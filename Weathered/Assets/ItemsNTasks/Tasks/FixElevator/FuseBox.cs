using UnityEngine;

public class FuseBox : Interaction
{
    public GameObject[] fuseBoxObjs;
    FixElevator fixElevator;

    public override void onClick()
    {
        if (fixElevator == null)
        {
            fixElevator = FindFirstObjectByType<FixElevator>();
        }

        fixElevator.ClickedFuseBox(this);
    }
}
