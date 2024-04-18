using UnityEngine;

public class MirrorCloth : Item
{
    [SerializeField] ClothObj clothObject;
    public void ClickedClothObject(ClothObj clothClicked)
    {
        ItemController.AddItemToHand(this);
        clothClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        clothObject.gameObject.SetActive(true);
    }
}
