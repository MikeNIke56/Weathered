public class Fuse : Item
{
    public FuseObj fuseObject;
    public void ClickedFuseObject(FuseObj fuseClicked)
    {
        ItemController.AddItemToHand(this);
        fuseClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        fuseObject.gameObject.SetActive(true);
    }
}
