using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIntro : MonoBehaviour
{
    public void GhostForm(bool isBall)
    {
        GetComponentInChildren<CelebritySpriteScript>().GhostBall(isBall);
    }
}
