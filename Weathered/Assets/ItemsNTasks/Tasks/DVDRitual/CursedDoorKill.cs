using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedDoorKill : MonoBehaviour
{
    float timer;
    public float maxTimer;

    private void Start()
    {
        timer = maxTimer;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timer -= Time.deltaTime;

            if(timer <= 0.0)
            {
                timer = maxTimer;
                Debug.Log("Player has died from poor danger management.");
                GameManager.StartDeath(null, 0f, false);
            }
        }
    }
}
