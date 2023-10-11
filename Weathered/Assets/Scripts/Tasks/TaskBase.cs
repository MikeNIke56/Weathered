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

    [SerializeField] Item[] possibleBoxItemsList;
    Item startItem;

    public Item[] itemsToSpawn;
    public GameObject itemObj;

    public bool returnable = false;


    public virtual string Name => name;

    public virtual string Description => description;

    public Sprite OverWorldIcon => overWorldIcon;
    public Sprite InspectIcon => inspectIcon;
    public Sprite StartItemIcon => startItemIcon;

    public Item[] PossibleBoxItemsList => possibleBoxItemsList;
    public Item StartItem => startItem;

    public virtual bool Display() {return true;}

    public virtual void ChooseStartItem()
    {
        startItem = possibleBoxItemsList[Random.Range(0, possibleBoxItemsList.Length)];
        startItemIcon = startItem.OverWorldIcon;

    }
}
