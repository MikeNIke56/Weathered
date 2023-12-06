using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathFlash : MonoBehaviour
{
    [SerializeField] Image BlackFade; 
    [SerializeField] Image RedFade;
    [SerializeField] float fadeIn;
    [SerializeField] float fadeFlash;
    [SerializeField] float fadeOut;
    void Start()
    {
        StartCoroutine(FlashEffect());
    }

    IEnumerator FlashEffect()
    {
        float inStep = fadeIn / 50;
        while (BlackFade.color.a < 1)
        {
            BlackFade.color += new Color(0f, 0f, 0f, 0.02f);
            yield return new WaitForSeconds(inStep);
        }
        float flashStep = fadeFlash / 50;
        while (RedFade.color.a < 1)
        {
            RedFade.color += new Color(0f, 0f, 0f, 0.02f);
            yield return new WaitForSeconds(flashStep);
        }
        BlackFade.gameObject.SetActive(false);
        GameManager.flashDone = true;
        float outStep = fadeOut / 50;
        while (RedFade.color.a > 0)
        {
            RedFade.color -= new Color(0f, 0f, 0f, 0.02f);
            yield return new WaitForSeconds(outStep);
        }
        Destroy(gameObject);
    }
}
