using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemBookObj : Interaction
{
    [SerializeField] PoemBook poemBook;

    public override void onClick()
    {
        if (poemBook == null)
        {
            poemBook = FindFirstObjectByType<PoemBook>();
        }
        poemBook.ClickedDVDObject(this);
    }
}
