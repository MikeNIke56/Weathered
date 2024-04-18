using UnityEngine;

public class FunDVD : Item
{
    [SerializeField] FunDVDObj ogFunDVDObj;
    public void ClickedDVDObject(FunDVDObj dvdClicked)
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
        ogFunDVDObj.gameObject.SetActive(true);
    }
}
