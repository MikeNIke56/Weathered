using UnityEngine;

public class SpiritLightHB : MonoBehaviour
{
    public float count = 2;
    bool isRefreshed = true;
    float maxCount = 2;

    private void Update()
    {
        if (isRefreshed == false)
        {
            count -= Time.deltaTime;

            if (count <= 0)
            {
                isRefreshed = true;
                count = maxCount;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /*count += Time.deltaTime;

            if(count >= maxCount)
            {
                Debug.Log("Player has died");
            }*/

            if (isRefreshed == true)
            {
                ShortTextController.STControl.AddShortText("Something is keeping me from opening this door...");
                isRefreshed = false;
            }
        }
    }
}
