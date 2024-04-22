using UnityEngine;

public class ArvensHits : Item
{
    [SerializeField] ArvensHitsObj ogArvenObj;


    public void ClickedDVDObject(ArvensHitsObj dvdClicked)
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
        ogArvenObj.gameObject.SetActive(true);
    }
    public void FindItem()
    {
        ogArvenObj = FindFirstObjectByType<ArvensHitsObj>();
    }
}
