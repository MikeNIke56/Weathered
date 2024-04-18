public class ArvinAutograph : Item
{
    public ArvinAutographObj arvinAutoObject;

    public void ClickedAutographObject(ArvinAutographObj autoClicked)
    {
        ItemController.AddItemToHand(this);
        autoClicked.gameObject.SetActive(false);
    }
    public void GiveAutographObject()
    {
        ItemController.AddItemToHand(this);
    }

    /*public override void OnDropped()
    {
        arvinLogic.autosOnGround++;
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();

        if(arvinLogic.autosOnGround > 0)
            arvinAutoObject.gameObject.SetActive(true);
        else
            arvinAutoObject.gameObject.SetActive(false);
    }*/
}
