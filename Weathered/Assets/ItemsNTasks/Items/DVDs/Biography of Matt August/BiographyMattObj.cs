using UnityEngine;

public class BiographyMattObj : Interaction
{
    [SerializeField] BiographyMatt mattBioDVD;

    public override void onClick()
    {
        if (mattBioDVD == null)
        {
            mattBioDVD = FindFirstObjectByType<BiographyMatt>();
        }
        mattBioDVD.ClickedDVDObject(this);
    }
}
