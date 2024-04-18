using UnityEngine;

public class TeabagsObj : Interaction
{
    [SerializeField] Teabags teaBags;

    public override void onClick()
    {
        if (teaBags == null)
        {
            teaBags = FindFirstObjectByType<Teabags>();
        }
        teaBags.ClickedTeaBagObject(this);
    }
}
