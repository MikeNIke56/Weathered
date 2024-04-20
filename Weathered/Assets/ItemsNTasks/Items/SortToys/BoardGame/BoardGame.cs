public class BoardGame : Item
{
    public override void ClearItem()
    {
        base.OnDropped();
    }
}
