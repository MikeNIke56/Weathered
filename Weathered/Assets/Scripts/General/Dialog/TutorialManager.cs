using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public bool IsShowing { get; private set; }

    public static TutorialManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ShowDialogText(string text, bool waitForInput = true, bool autoClose = true, List<string> choices = null, Action<int> onSelected = null)
    {
        IsShowing = true;

        dialogBox.SetActive(true);
        OnShowDialog?.Invoke();

        yield return TypeDialogue(text);

        if (waitForInput)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (autoClose)
        {
            CloseDialog();
        }
        OnCloseDialog?.Invoke();
    }

    public void CloseDialog()
    {
        dialogBox.SetActive(false);
        IsShowing = false;
    }

    public IEnumerator ShowDialog(Dialog dialog, Action onFinished = null, List<string> choices = null, Action<int> onSelected = null)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        IsShowing = true;
        UIController.UIControl.HandleTutorial(IsShowing);

        dialogBox.SetActive(true);

        foreach (var line in dialog.Lines)
        {
            yield return TypeDialogue(line);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        dialogBox.SetActive(false);
        IsShowing = false;
        OnCloseDialog?.Invoke();
    }

    public void HandleUpdate()
    {

    }

    public IEnumerator TypeDialogue(string line)
    {
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }
}
