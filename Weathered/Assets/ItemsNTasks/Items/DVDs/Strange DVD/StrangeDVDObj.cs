using UnityEngine;

public class StrangeDVDObj : Interaction
{
    [SerializeField] StrangeDVD strangeDVD;

    private void Start()
    {
        strangeDVD = FindFirstObjectByType<StrangeDVD>();
    }

    public override void onClick()
    {
        if (strangeDVD == null)
        {
            strangeDVD = FindFirstObjectByType<StrangeDVD>();
        }
        strangeDVD.ClickedDVDObject(this);
    }
}
