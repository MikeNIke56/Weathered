using UnityEngine;

public class ComedyDVDObj : Interaction
{
    [SerializeField] ComedyDVD comedyDVD;

    private void Start()
    {
        comedyDVD = FindFirstObjectByType<ComedyDVD>();
    }
    public override void onClick()
    {
        if (comedyDVD == null)
        {
            comedyDVD = FindFirstObjectByType<ComedyDVD>();
        }
        comedyDVD.ClickedDVDObject(this);
    }
}
