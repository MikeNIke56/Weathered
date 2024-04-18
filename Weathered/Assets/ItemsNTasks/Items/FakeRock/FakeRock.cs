public class FakeRock : Item
{
    public FakeRockObj fakeRockObject;
    public void ClickedRockObject(FakeRockObj rockClicked)
    {
        ItemController.AddItemToHand(this);
        rockClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        fakeRockObject.gameObject.SetActive(true);
    }
}
