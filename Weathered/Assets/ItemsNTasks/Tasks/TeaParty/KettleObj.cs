using UnityEngine;

public class KettleObj : Interaction
{
    [SerializeField] Kettle kettle;
    public AudioSource kettleWhistle;

    public override void onClick()
    {
        if (kettle == null)
        {
            kettle = FindFirstObjectByType<Kettle>();
        }
        kettle.ClickedKettleObject(this);
    }
}
