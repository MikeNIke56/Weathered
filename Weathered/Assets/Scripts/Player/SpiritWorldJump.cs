using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritWorldJump : MonoBehaviour
{
    static public SpiritWorldJump SWJ;
    static public bool isInSpiritWorld = false;
    [SerializeField] PlayerController playerControl;
    [SerializeField] Image blackoutImage;
    [SerializeField] Image vignetteImage;
    [SerializeField] float fadeInSeconds = 2f;
    static bool isJumping = false;
    void Start()
    {
        if (SWJ == null)
        {
            SWJ = FindFirstObjectByType<SpiritWorldJump>();
        }
        if (playerControl == null)
        {
            playerControl = FindFirstObjectByType<PlayerController>();
        }
    }
    static public void Jump()
    {
        if (isInSpiritWorld)
        {
            SWJ.JumpFromSpiritWorld();
        }
        else
        {
            SWJ.JumpToSpiritWorld();
        }
    }
    public void JumpToSpiritWorld()
    {
        if (!isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpToEffect());
        }
    }
    public void JumpFromSpiritWorld()
    {

        if (!isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpFromEffect());
        }
    }
    IEnumerator JumpToEffect()
    {
        //Remove player control
        playerControl.lockMovement = true;
        //FadeinBlackout
        float fadeStep = fadeInSeconds / 51;
        while (blackoutImage.color.a < 1)
        {
            blackoutImage.color += new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(fadeStep);
        }
        //PlayerPosMove
        playerControl.transform.position += new Vector3(0, -5000, 0);
        isInSpiritWorld = true;
        //EnableVignette
        vignetteImage.color = new Color(0,0,0,1);
        yield return new WaitForSeconds(1f);
        //FadeOutBlackout
        while (blackoutImage.color.a > 0)
        {
            blackoutImage.color -= new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(fadeStep);
        }
        //Regain player control
        playerControl.lockMovement = false;
        yield return new WaitForSeconds(1);
        isJumping = false;
    }
    IEnumerator JumpFromEffect()
    {
        //Remove player control
        playerControl.lockMovement = true;
        //FadeinBlackout
        float fadeStep = fadeInSeconds / 51;
        while (blackoutImage.color.a < 1)
        {
            blackoutImage.color += new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(fadeStep);
        }
        //PlayerPosMove
        playerControl.transform.position += new Vector3(0, 5000, 0);
        isInSpiritWorld = false;
        //EnableVignette
        vignetteImage.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(1f);
        //FadeOutBlackout
        while (blackoutImage.color.a > 0)
        {
            blackoutImage.color -= new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(fadeStep);
        }
        //Regain player control
        playerControl.lockMovement = false;
        yield return new WaitForSeconds(1);
        isJumping = false;
    }
}
