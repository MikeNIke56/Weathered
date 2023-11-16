using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOfLightbulbs : Interaction
{
    [SerializeField] Lightbulb lightbulb;
    [SerializeField] ReplaceLightBulb lightbulbTask;

    private void Awake()
    {

    }
    public override void onClick()
    {
        if (lightbulbTask == null)
        {
            lightbulbTask = FindFirstObjectByType<ReplaceLightBulb>();
        }

        lightbulb.ClickedLightbulbObject(lightbulb);
    } 
}
