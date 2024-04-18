using UnityEngine;

public class KeyEntranceObject : Interaction
{
    [SerializeField] KeyEntrance Key1Item;
    public override void onClick()
    {
        if (Key1Item == null)
        {
            Key1Item = FindFirstObjectByType<KeyEntrance>();
        }

        Key1Item.OnClickedKeyObject(this);
    }
}
