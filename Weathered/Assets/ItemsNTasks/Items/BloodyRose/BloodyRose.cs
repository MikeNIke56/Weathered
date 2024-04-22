public class BloodyRose : Item
{
    public BloodyRoseObj bloodyRoseObject;
    CelebAutoGraphs autoGraphs;

    public void ClickedRoseObject(BloodyRoseObj roseClicked)
    {
        autoGraphs = FindAnyObjectByType<CelebAutoGraphs>();
        bloodyRoseObject = FindAnyObjectByType<BloodyRoseObj>();
        ItemController.AddItemToHand(this);
        //roseClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        bloodyRoseObject.gameObject.SetActive(true);
    }
}
