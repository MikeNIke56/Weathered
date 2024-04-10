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
    public Dialog dialogAfterBoxFirst;
    public Dialog dialogAfterBoxSecond;
    public Dialog dialogBoxRight;

    public Dialog dialogStartCobwebs;
    public Dialog dialogInCobwebs;
    public Dialog dialogAfterCobwebsFirst;
    public Dialog dialogAfterCobwebsSecond;

    SortBoxes sortBoxes;
    DustCobwebs dustCobwebs;
    [SerializeField] DusterObject dustObject;

    bool startBoxLockout = false;
    bool tookToyLockout = false;
    bool isDoneBoxLockout = false;
    bool correctBoxLockout = false;

    bool pickedUpBroomLockout = false;
    bool clearedCobwebLockout = false;
    bool cobwebIsDoneLockout = false;

    public bool cobWebsFirst = false;
    public bool boxesFirst = false;

    public GameObject dialogBox;
    public GameObject cobwebsArrow;
    public GameObject boxesArrow;

    PlayerController player;

    public static TutorialDialog i { get; private set; }


    private void Awake()
    {
        i = this;
        sortBoxes = FindFirstObjectByType<SortBoxes>();
        dustCobwebs = FindFirstObjectByType<DustCobwebs>();
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
        //entrance animation 
        player.moveBlockers["TutorialDialog"] = true;
        yield return new WaitForSeconds(.5f);
        dialogBox.SetActive(true);

        //starting dialog
        yield return new WaitForSeconds(.5f);
        yield return TutorialManager.Instance.ShowDialog(introDialog);
        yield return new WaitForSeconds(.75f);

        cobwebsArrow.SetActive(true);
        boxesArrow.SetActive(true);

        yield return new WaitForSeconds(.35f);

        //unfreezes player
        player.moveBlockers["TutorialDialog"] = false;
        UIController.UIControl.ToggleInputHandler(false);
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

            if(boxesFirst == true)
            {
                UIController.UIControl.ToggleInputHandler(true);
                player.moveBlockers["TutorialDialog"] = true;
                yield return TutorialManager.Instance.ShowDialog(dialogAfterBoxFirst);
                cobwebsArrow.SetActive(true);
                player.moveBlockers["TutorialDialog"] = false;
                UIController.UIControl.ToggleInputHandler(false);
            } 
            else
            {
                yield return TutorialManager.Instance.ShowDialog(dialogAfterBoxSecond);
                UIController.UIControl.ToggleInputHandler(false);
                EndTutorial();
            }        
        }


        if (sortBoxes.correctBox == true && correctBoxLockout == false)
        {
            UIController.UIControl.ToggleInputHandler(true);
            sortBoxes.correctBox = false;
            correctBoxLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogBoxRight);
            UIController.UIControl.ToggleInputHandler(false);
        }

        if (dustObject.pickedUpBroom == true && pickedUpBroomLockout == false)
        {
            UIController.UIControl.ToggleInputHandler(true);
            dustObject.pickedUpBroom = false;
            pickedUpBroomLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogStartCobwebs);
            UIController.UIControl.ToggleInputHandler(false);
        }
        else if (dustCobwebs.clearedCobweb == true && clearedCobwebLockout == false)
        {
            UIController.UIControl.ToggleInputHandler(true);
            dustCobwebs.clearedCobweb = false;
            clearedCobwebLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogInCobwebs);
            UIController.UIControl.ToggleInputHandler(false);
        }
        else if (dustCobwebs.cobwebsDone == true && cobwebIsDoneLockout == false)
        {
            dustCobwebs.cobwebsDone = false;
            cobwebIsDoneLockout = true;

            if(cobWebsFirst == true)
            {
                UIController.UIControl.ToggleInputHandler(true);
                player.moveBlockers["TutorialDialog"] = true;
                yield return TutorialManager.Instance.ShowDialog(dialogAfterCobwebsFirst);
                boxesArrow.SetActive(true);
                player.moveBlockers["TutorialDialog"] = false;
                UIController.UIControl.ToggleInputHandler(false);
            }
            else
            {
                yield return TutorialManager.Instance.ShowDialog(dialogAfterCobwebsSecond);
                UIController.UIControl.ToggleInputHandler(false);
                EndTutorial();
            }
        }

        yield return null;
    }

    void EndTutorial()
    {
        boxesArrow.SetActive(false);
        cobwebsArrow.SetActive(false);
    }
}
