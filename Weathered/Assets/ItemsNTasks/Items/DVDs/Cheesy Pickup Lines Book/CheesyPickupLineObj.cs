using UnityEngine;

public class CheesyPickupLineObj : Interaction
{
    [SerializeField] CheesyPickupLine cheesyPickupLine;


    private void Start()
    {
        cheesyPickupLine = FindFirstObjectByType<CheesyPickupLine>();
    }
    public override void onClick()
    {
        if (cheesyPickupLine == null)
        {
            cheesyPickupLine = FindFirstObjectByType<CheesyPickupLine>();
        }
        cheesyPickupLine.ClickedDVDObject(this);
    }
}
