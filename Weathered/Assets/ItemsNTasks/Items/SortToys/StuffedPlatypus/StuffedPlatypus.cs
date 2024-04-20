public class StuffedPlatypus : Item
{
    public override void ClearItem()
    {
        base.OnDropped();
    }
}