using UnityEngine;

public class PoemBookObj : Interaction
{
    [SerializeField] PoemBook poemBook;

    private void Start()
    {
        poemBook = FindFirstObjectByType<PoemBook>();
    }
    public override void onClick()
    {
        if (poemBook == null)
        {
            poemBook = FindFirstObjectByType<PoemBook>();
        }
        poemBook.ClickedDVDObject(this);
    }
}
