using UnityEngine;

public class Mop : Item
{
    [SerializeField] MopObject originalMopObject;
    public AudioSource mopSfx;
    public void ClickedMopObject(MopObject mopClicked)
    {
        if (originalMopObject == null)
            originalMopObject = FindAnyObjectByType<MopObject>();

        ItemController.AddItemToHand(this);
        mopClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        originalMopObject.gameObject.SetActive(true);
    }
}
