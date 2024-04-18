using UnityEngine;

public class LightObj : Interaction
{
    ReplaceLightBulb lightbulbTask;
    public AudioSource screwSfx;

    public override void onClick()
    {
        if (lightbulbTask == null)
        {
            lightbulbTask = FindFirstObjectByType<ReplaceLightBulb>();
        }
        lightbulbTask.LightClicked();
    }
}
