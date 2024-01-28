using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepStool : Item
{
    [SerializeField]
    StepStoolObj stepStoolObj;

    public override void OnReplaced()
    {
        base.OnReplaced();
        stepStoolObj.gameObject.SetActive(true);
    }
}
