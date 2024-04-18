using UnityEngine;

public class DustWebsWeb : Interaction
{
    [SerializeField] DustCobwebs DCTask;

    public override void onClick()
    {
        if (DCTask == null)
        {
            DCTask = FindFirstObjectByType<DustCobwebs>();
        }

        DCTask.ClickedCobweb(this);
    }
}
