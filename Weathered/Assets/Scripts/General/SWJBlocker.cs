using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWJBlocker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpiritWorldJump.SWJ.jumpBlockers["NoJumpingZone"] = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpiritWorldJump.SWJ.jumpBlockers["NoJumpingZone"] = false;
        }
    }
}
