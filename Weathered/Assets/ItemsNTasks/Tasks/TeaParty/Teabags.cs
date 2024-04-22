public class Teabags : Item
{
    public TeabagsObj teaBagsObject;

    public void ClickedTeaBagObject(TeabagsObj teaBagClicked)
    {
        teaBagsObject = FindFirstObjectByType<TeabagsObj>();
        ItemController.AddItemToHand(this);
        teaBagClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        teaBagsObject.gameObject.SetActive(true);
    }
}
