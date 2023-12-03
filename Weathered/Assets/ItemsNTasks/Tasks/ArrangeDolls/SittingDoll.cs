using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingDoll : MonoBehaviour
{
    public List<Sprite> startLook;
    public List<Sprite> evilStartLook;
    public List<Sprite> fixedLook;
    public bool hasEvil = false;
    public SpriteRenderer sourceSR;
    public bool isEvil = false;
    public bool isFixed = false;

    public void PoseDoll(int facingNum)
    {
        if (isFixed)
        {
            if (facingNum > 0)
            {
                sourceSR.sprite = fixedLook[1];
            }
            else if (facingNum < 0)
            {
                sourceSR.sprite = fixedLook[2];
            }
            else
            {
                sourceSR.sprite = fixedLook[0];
            }
        }
        else if (isEvil && hasEvil)
        {
            if (facingNum > 0)
            {
                sourceSR.sprite = evilStartLook[1];
            }
            else if (facingNum < 0)
            {
                sourceSR.sprite = evilStartLook[2];
            }
            else
            {
                sourceSR.sprite = evilStartLook[0];
            }
        }
        else
        {
            if (facingNum > 0)
            {
                sourceSR.sprite = startLook[1];
            }
            else if (facingNum < 0)
            {
                sourceSR.sprite = startLook[2];
            }
            else
            {
                sourceSR.sprite = startLook[0];
            }
        }
    }
}
