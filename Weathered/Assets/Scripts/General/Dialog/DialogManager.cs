using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Image characterImg;
    [SerializeField] Text dialogText;

    [SerializeField] int lettersPerSecond;

    bool skipped = false;
    bool lineEnded = false;

    float skipTimer = 0.2f;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public bool IsShowing { get; private set; }

    public GameObject[] dialogImgs = new GameObject[7];

    public enum DialogTriggers { Mazarine, Celebrity, DollSong, Doll, Worker, Narrator, Aunt }; //all talkable characters
    public DialogTriggers trigger;

    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateTimer();
        if (IsShowing == true && lineEnded == false && Input.GetMouseButtonDown(0))
        {
            skipped = true;
        }
    }

    public IEnumerator ShowDialogText(string text, bool waitForInput=true, bool autoClose=true, List<string> choices = null, Action<int> onSelected = null)
    {
        IsShowing = true;

        dialogBox.SetActive(true);
        OnShowDialog?.Invoke();

        yield return TypeDialogue(text);

        if(waitForInput)
        {
           yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        }

        if (autoClose)
        {
            CloseDialog();
        }
        OnCloseDialog?.Invoke();
    }

    public void OpenDialog()
    {
        dialogBox.SetActive(true);
        IsShowing = true;
        UIController.UIControl.ToggleInputHandler(IsShowing);
    }
    public void CloseDialog()
    {
        dialogBox.SetActive(false);
        IsShowing = false;
        UIController.UIControl.ToggleInputHandler(IsShowing);
    }

    public IEnumerator ShowDialog(DialogTriggers character, string line)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();

        dialogBox.SetActive(true);

        switch (character)
        {
            case DialogTriggers.Mazarine:
                ToggleCharacterBox(0);
                break;
            case DialogTriggers.Celebrity:
                ToggleCharacterBox(1);
                break;
            case DialogTriggers.DollSong:
                ToggleCharacterBox(2);
                break;
            case DialogTriggers.Doll:
                ToggleCharacterBox(3);
                break;
            case DialogTriggers.Aunt:
                ToggleCharacterBox(6);
                break;
            case DialogTriggers.Narrator:
                ToggleCharacterBox(5);
                break;
            case DialogTriggers.Worker:
                ToggleCharacterBox(4);
                break;
            default:
                ToggleCharacterBox(0);
                break;
        }

        yield return TypeDialogue(line);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && skipTimer == 0.0f);
        skipTimer = 0.2f;
        skipped = false;

        OnCloseDialog?.Invoke();
    }

    void ToggleCharacterBox(int num)
    {
        for (int i = 0; i < dialogImgs.Length; i++)
        {
            if(i == num)
                dialogImgs[i].SetActive(true);
            else
                dialogImgs[i].SetActive(false);
        }
    }


    public IEnumerator ShowDialog(DialogTriggers character, Dialog dialog)
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
