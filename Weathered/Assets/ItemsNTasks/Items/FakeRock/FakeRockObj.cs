using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeRockObj : Interaction
{
    [SerializeField] FakeRock fakeRock;

    public override void onClick()
    {
        if (fakeRock == null)
        {
            fakeRock = FindFirstObjectByType<FakeRock>();
        }
        fakeRock.ClickedRockObject(this);
    }
}
