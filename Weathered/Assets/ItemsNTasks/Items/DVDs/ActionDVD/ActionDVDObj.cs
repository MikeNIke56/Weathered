using UnityEngine;

public class ActionDVDObj : Interaction
{
    [SerializeField] ActionDVD actionDVD;

    public override void onClick()
    {
        if (actionDVD == null)
        {
            actionDVD = FindFirstObjectByType<ActionDVD>();
        }
        actionDVD.ClickedDVDObject(this);
    }
}
