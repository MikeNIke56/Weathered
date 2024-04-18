using UnityEngine;

public class SWEntranceCollision : MonoBehaviour
{
    SpiritWorldJump SWJ;
    bool isStarting = true;
    void Start()
    {
        if (SWJ == null)
        {
            SWJ = FindFirstObjectByType<SpiritWorldJump>();
        }
        isStarting = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isStarting)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            SWJ.isEntrance = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SWJ.isEntrance = false;
        }
    }
}
