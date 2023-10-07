using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Sprite overWorldIcon;

    public bool isPartOfTask = false;

    public virtual bool Display() { return false; }

    public virtual string Name => name;

    public virtual string Description => description;

    public Sprite OverWorldIcon => overWorldIcon;
}
