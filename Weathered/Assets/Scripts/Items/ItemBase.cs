using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Sprite overWorldIcon;
    [SerializeField] Sprite inspectIcon;

    public virtual string Name => name;

    public virtual string Description => description;

    public Sprite OverWorldIcon => overWorldIcon;
    public Sprite InspectIcon => inspectIcon;

    public virtual bool Display()
    {
        return false;
    }
}
