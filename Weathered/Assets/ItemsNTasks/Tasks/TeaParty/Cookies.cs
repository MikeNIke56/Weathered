public class Cookies : Item
{
    public CookiesObj cookiesObject;

    public void ClickedCookiesObject(CookiesObj cookiesClicked)
    {
        cookiesObject = FindFirstObjectByType<CookiesObj>();
        ItemController.AddItemToHand(this);
        cookiesClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        cookiesObject.gameObject.SetActive(true);
    }
}
