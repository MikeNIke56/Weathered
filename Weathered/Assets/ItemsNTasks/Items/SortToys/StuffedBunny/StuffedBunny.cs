public class StuffedBunny : Item
{
    public override void ClearItem()
    {
        base.OnDropped();
    }
}