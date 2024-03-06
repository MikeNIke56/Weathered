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
    [SerializeField] Image deathEffectImage;
    [SerializeField] Image vignetteImage;
    [SerializeField] float fadeInSeconds = 2f;
    public bool isEntrance = false;
    static bool isJumping = false;
    public Dictionary<string, bool> jumpBlockers = new Dictionary<string, bool>();
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
        jumpBlockers.Add("CassetteSpirit", false);

    }
    static public void Jump()
    {
        bool blockJump = false;
        if (SWJ.jumpBlockers["CassetteSpirit"])
        {
            FindFirstObjectByType<MusicBox>().JumpFailed();
            blockJump = true;
        }

        if (blockJump)
        {
            return;
        }
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
        playerControl.moveBlockers["SpiritWorldTransition"] = true;
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
        BGMManager.BGM.SwitchWorldBGM(true);
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
        playerControl.moveBlockers["SpiritWorldTransition"] = false;
        yield return new WaitForSeconds(1);
        isJumping = false;
        StartCoroutine(DeathCountDown());
    }
    IEnumerator JumpFromEffect()
    {
        //Remove player control
        playerControl.moveBlockers["SpiritWorldTransition"] = true;
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
        BGMManager.BGM.SwitchWorldBGM(false);
        //EnableVignette
        deathEffectImage.color = new Color32(0, 0, 0, 0);
        vignetteImage.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(1f);
        //FadeOutBlackout
        while (blackoutImage.color.a > 0)
        {
            blackoutImage.color -= new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(fadeStep);
        }
        //Regain player control
        playerControl.moveBlockers["SpiritWorldTransition"] = false;
        yield return new WaitForSeconds(1);
        isJumping = false;
    }

    IEnumerator DeathCountDown()
    {
        bool isEscaped = false;
        deathEffectImage.color = new Color32(0, 0, 0, 0);
        float fadeStep = 20f / 255f;
        while (deathEffectImage.color.a < 1)
        {
            if (!isInSpiritWorld || isInSpiritWorld && isJumping)
            {
                isEscaped = true;
                break;
            }
            if (isEntrance)
            {
                deathEffectImage.color -= new Color32(0, 0, 0, 1);
            }
            else
            {
                deathEffectImage.color += new Color32(0, 0, 0, 1);
            }
            yield return new WaitForSeconds(fadeStep);
        }
        if (isEscaped)
        {

        }
        else
        {
            //Kill player
            Debug.Log("Player has failed to escape the spirit world.");
            GameManager.StartDeath(null, 0f, false);
        }
    }
}
