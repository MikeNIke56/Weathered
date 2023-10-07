using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Sprite overWorldIcon;
    [SerializeField] Sprite inspectIcon;
    Sprite startItemIcon;

    [SerializeField] Item[] startItemList;
    Item startItem;

    public Item[] itemsToSpawn;
    public GameObject itemObj;

    public virtual string Name => name;

    public virtual string Description => description;

    public Sprite OverWorldIcon => overWorldIcon;
    public Sprite InspectIcon => inspectIcon;
    public Sprite StartItemIcon => startItemIcon;

    public Item[] StartItemList => startItemList;
    public Item StartItem => startItem;

    public virtual bool Display() {return true;}

    public virtual void ChooseStartItem()
    {
        startItem = startItemList[Random.Range(0, startItemList.Length)];
        startItemIcon = startItem.OverWorldIcon;

    }
}
