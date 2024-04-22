using UnityEngine;

public class ActionDVDObj : Interaction
{
    [SerializeField] ActionDVD actionDVD;

    private void Start()
    {
        actionDVD = FindFirstObjectByType<ActionDVD>();
    }
    public override void onClick()
    {
        if (actionDVD == null)
        {
            actionDVD = FindFirstObjectByType<ActionDVD>();
        }
        actionDVD.ClickedDVDObject(this);
    }
}
