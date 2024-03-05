using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfInteract : Interaction
{
    ArrangeSnowglobes snowglobes;

    private void Start()
    {
        snowglobes = FindAnyObjectByType<ArrangeSnowglobes>();
    }

    public override void onClick()
    {
        snowglobes.ShelfClicked();
        snowglobes.quitButton.SetActive(true);
    }
}
