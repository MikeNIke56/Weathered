using UnityEngine;

public class ObstructPlayer : MonoBehaviour
{
    [SerializeField]
    bool isObstructing = true;
    bool isCurrentlyObstructing = false;
    PlayerController pc;
    float playerContactPosDelta = 0f;

    void FixedUpdate()
    {
        if (!isObstructing && isCurrentlyObstructing)
        {
            isCurrentlyObstructing = false;
            playerContactPosDelta = gameObject.transform.position.x - pc.transform.position.x;
            if (playerContactPosDelta > 0)
            {
                pc.rightBlocked = false;
            }
            else
            {
                pc.leftBlocked = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isObstructing)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            isCurrentlyObstructing = true;
            playerContactPosDelta = gameObject.transform.position.x - pc.transform.position.x;
            if (playerContactPosDelta > 0)
            {
                pc.rightBlocked = true;
            }
            else
            {
                pc.leftBlocked = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!isObstructing)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            isCurrentlyObstructing = false;
            playerContactPosDelta = gameObject.transform.position.x - pc.transform.position.x;
            if (playerContactPosDelta > 0)
            {
                pc.rightBlocked = false;
            }
            else
            {
                pc.leftBlocked = false;
            }
        }
    }

    void OnEnable()
    {
        pc = FindFirstObjectByType<PlayerController>();
    }
}
