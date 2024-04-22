using UnityEngine;

public class PoemBook : Item
{
    [SerializeField] PoemBookObj ogPoemBookOVObj;


    public void ClickedDVDObject(PoemBookObj dvdClicked)
    {
        FindItem();
        ItemController.AddItemToHand(this);
        dvdClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        ogPoemBookOVObj.gameObject.SetActive(true);
    }
    public void FindItem()
    {
        ogPoemBookOVObj = FindFirstObjectByType<PoemBookObj>();
    }
}
