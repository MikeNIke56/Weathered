using UnityEngine;

public class ThrillingDVDObj : Interaction
{
    [SerializeField] ThrillingDVD thrillingDVD;

    public override void onClick()
    {
        if (thrillingDVD == null)
        {
            thrillingDVD = FindFirstObjectByType<ThrillingDVD>();
        }
        thrillingDVD.ClickedDVDObject(this);
    }
}
