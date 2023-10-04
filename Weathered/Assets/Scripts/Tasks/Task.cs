using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Task/Create new Task")]
public class Task : TaskBase
{
    [SerializeField] Item[] itemsToSpawn; 
    public Transform[] spawnPoints;

    public override void StartTask()
    {
        isActive = true;
        Debug.Log("task started");
    }

    public Item[] ItemsToSpawn => itemsToSpawn;
}
