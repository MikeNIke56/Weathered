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
    [SerializeField] GameObject fadeObj;
    GameManager gameManager;

    bool skipped = false;
    bool lineEnded = false;

    float skipTimer = 0.2f;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public bool IsShowing { get; private set; }

    public static TutorialManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>(FindObjectsInactive.Include);

        if (gameManager.tutorialPlayed == false)
        {
            fadeObj.SetActive(true);
            gameManager.tutorialPlayed = true;
        }
    }

    private void Update()
    {
        UpdateTimer();
        if (IsShowing == true && lineEnded == false && Input.GetMouseButtonDown(0))
        {
            skipped = true;
        }
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
        UIController.UIControl.ToggleInputHandler(IsShowing);

        dialogBox.SetActive(true);

        foreach (var line in dialog.Lines)
        {
            yield return TypeDialogue(line);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && skipTimer == 0.0f);
            skipTimer = 0.2f;
            skipped = false;
        }

        dialogBox.SetActive(false);
        IsShowing = false;
        OnCloseDialog?.Invoke();
    }

    public IEnumerator TypeDialogue(string line)
    {
        dialogText.text = "";
        lineEnded = false;

        foreach (var letter in line.ToCharArray())
        {
            if (skipped == false)
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(1f / lettersPerSecond);
            }
            else
            {
                dialogText.text += letter;
            }
            skipTimer = 0.2f;
        }
        lineEnded = true;
    }

    void UpdateTimer()
    {
        if (skipTimer > 0)
        {
            skipTimer = Mathf.Clamp(skipTimer - Time.deltaTime, 0, skipTimer);
        }
    }
}
