using UnityEngine;

public class ComedyDVDObj : Interaction
{
    [SerializeField] ComedyDVD comedyDVD;

    public override void onClick()
    {
        if (comedyDVD == null)
        {
            comedyDVD = FindFirstObjectByType<ComedyDVD>();
        }
        comedyDVD.ClickedDVDObject(this);
    }
}
