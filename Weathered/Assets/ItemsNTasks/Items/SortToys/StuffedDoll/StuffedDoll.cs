public class StuffedDoll : Item
{
    public override void ClearItem()
    {
        base.OnDropped();
    }
}