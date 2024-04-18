using UnityEngine;

public class ActionDVD : Item
{
    [SerializeField] ActionDVDObj originalActionDVDObject;

    public void ClickedDVDObject(ActionDVDObj dvdClicked)
    {
        ItemController.AddItemToHand(this);
        dvdClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        originalActionDVDObject.gameObject.SetActive(true);
    }
}
