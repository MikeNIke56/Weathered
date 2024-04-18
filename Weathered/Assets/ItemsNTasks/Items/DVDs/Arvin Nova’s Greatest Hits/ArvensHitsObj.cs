using UnityEngine;

public class ArvensHitsObj : Interaction
{
    [SerializeField] ArvensHits arvensHits;

    public override void onClick()
    {
        if (arvensHits == null)
        {
            arvensHits = FindFirstObjectByType<ArvensHits>();
        }
        arvensHits.ClickedDVDObject(this);
    }
}
