public class KeyDVDObject : Interaction
{
    public override void onClick()
    {
        ItemController.AddItemToHand(FindFirstObjectByType<KeyDVD>());
        Destroy(gameObject);
    }
}
