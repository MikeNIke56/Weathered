using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    public bool isActive = false;

    public virtual string Name => name;

    public virtual string Description => description;

    public virtual void StartTask()
    {
        isActive = false;
    }
}
