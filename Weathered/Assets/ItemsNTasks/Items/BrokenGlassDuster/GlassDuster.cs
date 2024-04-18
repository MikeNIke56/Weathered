using UnityEngine;

public class GlassDuster : Item
{
    [SerializeField] GlassDusterObject originalDusterObject;
    public AudioSource sweepSfx;
    public void ClickedDusterObject(GlassDusterObject dusterClicked)
    {
        if (originalDusterObject == null)
            originalDusterObject = FindAnyObjectByType<GlassDusterObject>();

        ItemController.AddItemToHand(this);
        dusterClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        originalDusterObject.gameObject.SetActive(true);
    }
}
