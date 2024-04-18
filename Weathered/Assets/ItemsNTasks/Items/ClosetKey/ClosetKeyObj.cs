using UnityEngine;

public class ClosetKeyObject : Interaction
{
    [SerializeField] ClosetKey keyItem;
    public override void onClick()
    {
        if (keyItem == null)
        {
            keyItem = FindFirstObjectByType<ClosetKey>();
        }

        keyItem.OnClickedKeyObject(this);
    }
}
