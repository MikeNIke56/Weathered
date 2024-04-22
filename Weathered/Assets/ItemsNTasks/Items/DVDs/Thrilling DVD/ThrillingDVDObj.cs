using UnityEngine;

public class ThrillingDVDObj : Interaction
{
    [SerializeField] ThrillingDVD thrillingDVD;

    private void Start()
    {
        thrillingDVD = FindFirstObjectByType<ThrillingDVD>();
    }

    public override void onClick()
    {
        if (thrillingDVD == null)
        {
            thrillingDVD = FindFirstObjectByType<ThrillingDVD>();
        }
        thrillingDVD.ClickedDVDObject(this);
    }
}
