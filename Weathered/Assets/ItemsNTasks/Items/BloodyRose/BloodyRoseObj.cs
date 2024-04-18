using UnityEngine;

public class BloodyRoseObj : Interaction
{
    [SerializeField] BloodyRose bloodyRose;

    public override void onClick()
    {
        if (bloodyRose == null)
        {
            bloodyRose = FindFirstObjectByType<BloodyRose>();
        }
        bloodyRose.ClickedRoseObject(this);
    }
}
