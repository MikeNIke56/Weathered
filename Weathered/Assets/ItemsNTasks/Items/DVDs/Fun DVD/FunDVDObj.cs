using UnityEngine;

public class FunDVDObj : Interaction
{
    [SerializeField] FunDVD funDVD;

    private void Start()
    {
        funDVD = FindFirstObjectByType<FunDVD>();
    }
    public override void onClick()
    {
        if (funDVD == null)
        {
            funDVD = FindFirstObjectByType<FunDVD>();
        }
        funDVD.ClickedDVDObject(this);
    }
}
