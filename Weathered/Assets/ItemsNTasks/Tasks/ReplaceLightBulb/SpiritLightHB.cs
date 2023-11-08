using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritLightHB : MonoBehaviour
{
    public float count = 0;
    float maxCount = 1.5f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            count += Time.deltaTime;

            if(count >= maxCount)
            {
                Debug.Log("Player has died");
            }
        }
    }
}
