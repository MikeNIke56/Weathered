public class WateringCan : Item
{
    public WaterCanOVObj wateringCanObject;

    public void ClickedWateringCanObject(WaterCanOVObj canClicked)
    {
        wateringCanObject = FindFirstObjectByType<WaterCanOVObj>();
        ItemController.AddItemToHand(this);
        canClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        wateringCanObject.gameObject.SetActive(true);
    }
}
