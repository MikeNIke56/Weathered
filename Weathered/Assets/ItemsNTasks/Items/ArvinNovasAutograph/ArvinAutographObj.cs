using UnityEngine;

public class ArvinAutographObj : Interaction
{
    [SerializeField] ArvinAutograph arvinAuto;

    public override void onClick()
    {
        if (arvinAuto == null)
        {
            arvinAuto = FindFirstObjectByType<ArvinAutograph>();
        }
        arvinAuto.ClickedAutographObject(this);
    }
}
