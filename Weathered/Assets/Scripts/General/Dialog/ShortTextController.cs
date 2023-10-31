using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortTextController : MonoBehaviour
{
    static public ShortTextController STControl;
    [SerializeField] RectTransform shortTextContainer;
    [SerializeField] Text shortTextText;
    Image backImage;
    [SerializeField] Color32 defaultColor;
    [SerializeField] float fadeLengthInSeconds = 2f;
    [SerializeField] float textAppearSpeedInSeconds = 0.05f;
    [SerializeField] float textHangTimeInSeconds = 5f;
    static bool isShowing = false;
    static public List<string> textLines = new List<string>();
    static bool isTextProgressiveRunning = false;
    static bool skipTextProgress = false;
    static bool currentlyProgressing = false;
    static bool isDoneHanging = false;

    void Start()
    {
        STControl = FindFirstObjectByType<ShortTextController>();
        backImage = shortTextContainer.GetComponent<Image>();
        backImage.color -= new Color(0,0,0,1);
        shortTextText.text = "";
        if (!isTextProgressiveRunning)
        {
            StartCoroutine(textProgressive());
        }
    }
    public void AddShortText(string textToAdd)
    {
        AddShortText(textToAdd, false);
    }
    public void AddShortText(string textToAdd, bool doClearText)
    {
        if (doClearText && currentlyProgressing)
        {
            textLines.Clear();
            skipTextProgress = true;
        }

        textLines.Add(textToAdd);
    }
    IEnumerator textProgressive()
    {
        isTextProgressiveRunning = true;
        string currentLine = "";
        string textLeft = "";
        while (true)
        {
            if (textLines.Count > 0)
            {
                currentlyProgressing = true;
                if (!isShowing)
                {
                    ShowBubble();
                }
                currentLine = textLines[0];
                textLeft = currentLine;
                textLines.RemoveAt(0);
                while (textLeft.Length > 1)
                {
                    if (skipTextProgress)
                    {
                        currentLine = "";
                        break;
                    }
                    shortTextText.text += textLeft.Substring(0, 1);
                    textLeft = textLeft.Substring(1);
                    yield return new WaitForSeconds(textAppearSpeedInSeconds);
                }
                shortTextText.text = currentLine;
                if (!skipTextProgress)
                {
                    isDoneHanging = false;
                    StartCoroutine(WaitHangTimer());
                    yield return new WaitUntil(() => isDoneHanging || skipTextProgress);
                }
                else
                {
                    skipTextProgress = false;
                    StopCoroutine(WaitHangTimer());
                }

                shortTextText.text = "";
                currentLine = "";
                if (textLines.Count <= 0)
                {
                    HideBubble();
                }
                currentlyProgressing = false;
            }
            else
            {
                yield return new WaitUntil(() => textLines.Count > 0);
            }
        }
    }
    IEnumerator WaitHangTimer()
    {
        yield return new WaitForSeconds(textHangTimeInSeconds);
        isDoneHanging = true;
    }
    public void ShowBubble()
    {
        ShowBubble(false);
    }
    public void ShowBubble(bool skipFade)
    {
        isShowing = true;
        if (skipFade)
        {
            backImage.color += new Color(0, 0, 0, 1);
        }
        else
        {
            StartCoroutine(FadeBubble(true));
        }
    }

    public void HideBubble()
    {
        HideBubble(false);
    }
    public void HideBubble(bool skipFade)
    {
        isShowing = false;
        if (skipFade)
        {
            backImage.color += new Color(0,0,0,1);
        }
        else
        {
            StartCoroutine(FadeBubble(false));
        }
    }

    IEnumerator FadeBubble(bool isFadingToVisible)
    {
        float fadeStep = fadeLengthInSeconds / 51;
        if (isFadingToVisible)
        {
            while (backImage.color.a < 1)
            {
                if (!isShowing)
                {
                    break;
                }
                backImage.color += new Color32(0, 0, 0, 5);
                yield return new WaitForSeconds(fadeStep);
            }
        }
        else
        {
            while (backImage.color.a > 0)
            {
                if (isShowing)
                {
                    break;
                }
                backImage.color -= new Color32(0, 0, 0, 5);
                yield return new WaitForSeconds(fadeStep);
            }
        }
    }
}
