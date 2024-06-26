using UnityEngine;

public class StrangeDVD : Item
{
    [SerializeField] StrangeDVDObj ogStrangeDVDObj;

    public void ClickedDVDObject(StrangeDVDObj dvdClicked)
    {
        FindItem();
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
        ogStrangeDVDObj.gameObject.SetActive(true);
    }
    public void FindItem()
    {
        ogStrangeDVDObj = FindFirstObjectByType<StrangeDVDObj>();
    }
}
