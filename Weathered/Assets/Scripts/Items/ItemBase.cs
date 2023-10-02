using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Sprite overWorldIcon;
    [SerializeField] Sprite inspectIcon;
    [SerializeField] Sprite itemIcon;
    [SerializeField] TaskBase task;

    public virtual string Name => name;

    public virtual string Description => description;

    public virtual TaskBase Task => task;

    public Sprite OverWorldIcon => overWorldIcon;
    public Sprite InspectIcon => inspectIcon;
    public Sprite ItemIcon => itemIcon;

    public virtual bool Display() {return false;}
}
