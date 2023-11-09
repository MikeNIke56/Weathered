using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxBox : Interaction
{
    [SerializeField] MusicBox musicTask;
    Item currentCassette;
    public override void onClick()
    {
        if (musicTask == null)
        {
            musicTask = FindFirstObjectByType<MusicBox>();
        }
        musicTask.ClickedBox(this);
    }
}
