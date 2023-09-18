using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer SR;
    [SerializeField]
    float fadeSpeed = 1f;
    bool isFadingIn = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isFadingIn)
            {
                StartCoroutine("FadeOut");
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isFadingIn)
            {
                isFadingIn = true;
                StartCoroutine("FadeIn");
            }
        }
    }

    IEnumerator FadeOut()
    {
        while (SR.color.a > 0 && !isFadingIn)
        {
            SR.color -= new Color(0f, 0f, 0f, 0.01f * fadeSpeed);
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator FadeIn()
    {
        while (SR.color.a < 1)
        {
            SR.color += new Color(0f, 0f, 0f, 0.01f * fadeSpeed);
            yield return new WaitForSeconds(0.02f);
        }
        isFadingIn = false;
    }
}
