using UnityEngine;

public class ArvensHitsObj : Interaction
{
    [SerializeField] ArvensHits arvensHits;

    private void Start()
    {
        arvensHits = FindFirstObjectByType<ArvensHits>();
    }
    public override void onClick()
    {
        if (arvensHits == null)
        {
            arvensHits = FindFirstObjectByType<ArvensHits>();
        }
        arvensHits.ClickedDVDObject(this);
    }
}
