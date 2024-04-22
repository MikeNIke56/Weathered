public class StatueHead : Item
{
    public StatueHeadObj statueObject;

    public void ClickedStatueObject(StatueHeadObj headClicked)
    {
        statueObject = FindFirstObjectByType<StatueHeadObj>();
        ItemController.AddItemToHand(this);
        headClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        statueObject.gameObject.SetActive(true);
    }
}
