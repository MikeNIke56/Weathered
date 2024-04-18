using UnityEngine;

public class ClosetInteract : Interaction
{
    [SerializeField] GameObject itemToGive;
    [SerializeField] GameObject itemToGive2;
    [SerializeField] Sprite doorOpen;
    [SerializeField] AudioSource doorOpenSfx;
    [SerializeField] AudioSource doorLockedSfx;

    public bool isLocked = true;

    public override void onClick()
    {
        if (isLocked)
        {
            ShortTextController.STControl.AddShortText("There's something keeping the door shut...");
            doorLockedSfx.Play();
        }
        else
        {
            ShortTextController.STControl.HideBubble();
            itemToGive.SetActive(true);
            itemToGive2.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
            doorOpenSfx.Play();
            gameObject.GetComponent<Interactable>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void OnCompletedLoad()
    {
        itemToGive.SetActive(true);
        itemToGive2.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
        gameObject.GetComponent<Interactable>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        isLocked = false;
    }
}
