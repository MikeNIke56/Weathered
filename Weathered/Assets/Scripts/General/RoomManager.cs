using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer SR;
    [SerializeField]
    float fadeSpeed = 1f;
    bool HasStartedFading = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !HasStartedFading)
        {
            HasStartedFading = true;
            BGMManager.BGM.ProgressTrack();
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        while (SR.color.a > 0)
        {
            SR.color -= new Color(0f, 0f, 0f, 0.01f * fadeSpeed);
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }
}
