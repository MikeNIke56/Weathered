using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class TutorialDialog : MonoBehaviour
{
    public Dialog introDialog;
    public Dialog dialogStartBox;
    public Dialog dialogInBox;
    public Dialog dialogAfterBoxSecond;
    public Dialog dialogBoxRight;

    SortBoxes sortBoxes;

    bool startBoxLockout = false;
    bool tookToyLockout = false;
    bool isDoneBoxLockout = false;
    bool correctBoxLockout = false;

    public bool boxesFirst = false;

    public GameObject dialogBox;
    public GameObject boxesArrow;

    PlayerController player;

    public static TutorialDialog i { get; private set; }


    private void Awake()
    {
        i = this;
        sortBoxes = FindFirstObjectByType<SortBoxes>();
        player = FindFirstObjectByType<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDialog());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(HandleUpdate());
    }
    public IEnumerator StartDialog()
    {
        UIController.UIControl.isCamFree = false;
        player.moveBlockers["TutorialDialog"] = true;
        GameManager.GM.HideDeathScreen();

        //entrance animation 
        yield return new WaitForSeconds(.5f);
        dialogBox.SetActive(true);

        //starting dialog
        yield return new WaitForSeconds(.5f);
        yield return TutorialManager.Instance.ShowDialog(introDialog);
        yield return new WaitForSeconds(.75f);

        boxesArrow.SetActive(true);

        yield return new WaitForSeconds(.35f);

        //unfreezes player
        player.moveBlockers["TutorialDialog"] = false;
        UIController.UIControl.ToggleInputHandler(false);
        UIController.UIControl.isCamFree = true;
    }

    public IEnumerator HandleUpdate()
    {
        if (sortBoxes.startBox == true && startBoxLockout == false)
        {
            UIController.UIControl.ToggleInputHandler(true);
            sortBoxes.startBox = false;
            startBoxLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogStartBox);
            UIController.UIControl.ToggleInputHandler(false);
        }
        else if (sortBoxes.tookToy == true && tookToyLockout == false)
        {
            UIController.UIControl.ToggleInputHandler(true);
            sortBoxes.tookToy = false;
            tookToyLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogInBox);
            UIController.UIControl.ToggleInputHandler(false);
        }
        else if (sortBoxes.isDoneBox == true && isDoneBoxLockout == false)
        {
            sortBoxes.isDoneBox = false;
            isDoneBoxLockout = true;

            yield return TutorialManager.Instance.ShowDialog(dialogAfterBoxSecond);
            UIController.UIControl.ToggleInputHandler(false);
            EndTutorial();      
        }


        if (sortBoxes.correctBox == true && correctBoxLockout == false)
        {
            UIController.UIControl.ToggleInputHandler(true);
            sortBoxes.correctBox = false;
            correctBoxLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogBoxRight);
            UIController.UIControl.ToggleInputHandler(false);
        }

        yield return null;
    }

    void EndTutorial()
    {
        boxesArrow.SetActive(false);
    }
}
