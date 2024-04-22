using UnityEngine;

public class BiographyMattObj : Interaction
{
    [SerializeField] BiographyMatt mattBioDVD;

    private void Start()
    {
        mattBioDVD = FindFirstObjectByType<BiographyMatt>();
    }
    public override void onClick()
    {
        if (mattBioDVD == null)
        {
            mattBioDVD = FindFirstObjectByType<BiographyMatt>();
        }
        mattBioDVD.ClickedDVDObject(this);
    }
}
