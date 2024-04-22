using UnityEngine;

public class ThrillingDVD : Item
{
    [SerializeField] ThrillingDVDObj ogThrillingDVDObj;


    public void ClickedDVDObject(ThrillingDVDObj dvdClicked)
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
        ogThrillingDVDObj.gameObject.SetActive(true);
    }
    public void FindItem()
    {
        ogThrillingDVDObj = FindFirstObjectByType<ThrillingDVDObj>();
    }
}
