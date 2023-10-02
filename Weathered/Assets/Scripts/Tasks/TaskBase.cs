using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Sprite overWorldIcon;
    [SerializeField] Sprite inspectIcon;
    [SerializeField] Sprite startItemIcon;

    public bool isActive = false;

    public virtual string Name => name;

    public virtual string Description => description;

    public Sprite OverWorldIcon => overWorldIcon;
    public Sprite InspectIcon => inspectIcon;
    public Sprite StartItemIcon => startItemIcon;

    public virtual bool Display() {return true;}
    public virtual void StartTask()
    {
        isActive = true;
    }
}
