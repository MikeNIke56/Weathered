using UnityEngine;

public class StrangeDVDObj : Interaction
{
    [SerializeField] StrangeDVD strangeDVD;

    public override void onClick()
    {
        if (strangeDVD == null)
        {
            strangeDVD = FindFirstObjectByType<StrangeDVD>();
        }
        strangeDVD.ClickedDVDObject(this);
    }
}
