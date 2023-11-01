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
    float speedB4Pause;

    public static TutorialDialog i { get; private set; }


    private void Awake()
    {
        i = this;
        sortBoxes = FindFirstObjectByType<SortBoxes>();
        dustCobwebs = FindFirstObjectByType<DustCobwebs>();
        player = FindFirstObjectByType<PlayerController>();

        speedB4Pause = player.moveSpeed;
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
        yield return new WaitForSeconds(.5f);
        dialogBox.SetActive(true);
        player.lockMovement = true;

        //starting dialog
        yield return new WaitForSeconds(.5f);
        yield return TutorialManager.Instance.ShowDialog(introDialog);
        yield return new WaitForSeconds(.75f);

        cobwebsArrow.SetActive(true);
        boxesArrow.SetActive(true);

        yield return new WaitForSeconds(.35f);

        //unfreezes player
        player.lockMovement = false;
    }

    public IEnumerator HandleUpdate()
    {
        if (sortBoxes.startBox == true && startBoxLockout == false)
        {
            sortBoxes.startBox = false;
            startBoxLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogStartBox);
        }
        else if (sortBoxes.tookToy == true && tookToyLockout == false)
        {
            sortBoxes.tookToy = false;
            tookToyLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogInBox);
        }
        else if (sortBoxes.isDoneBox == true && isDoneBoxLockout == false)
        {
            sortBoxes.isDoneBox = false;
            isDoneBoxLockout = true;

            if(boxesFirst == true)
            {
                player.lockMovement = true;
                yield return TutorialManager.Instance.ShowDialog(dialogAfterBoxFirst);
                cobwebsArrow.SetActive(true);
                player.lockMovement = false;
            } 
            else
                yield return TutorialManager.Instance.ShowDialog(dialogAfterBoxSecond);         
        }


        if (sortBoxes.correctBox == true && correctBoxLockout == false)
        {
            sortBoxes.correctBox = false;
            correctBoxLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogBoxRight);
        }

        if (dustObject.pickedUpBroom == true && pickedUpBroomLockout == false)
        {
            dustObject.pickedUpBroom = false;
            pickedUpBroomLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogStartCobwebs);
        }
        else if (dustCobwebs.clearedCobweb == true && clearedCobwebLockout == false)
        {
            dustCobwebs.clearedCobweb = false;
            clearedCobwebLockout = true;
            yield return TutorialManager.Instance.ShowDialog(dialogInCobwebs);
        }
        else if (dustCobwebs.cobwebsDone == true && cobwebIsDoneLockout == false)
        {
            dustCobwebs.cobwebsDone = false;
            cobwebIsDoneLockout = true;

            if(cobWebsFirst == true)
            {
                player.lockMovement = true;
                yield return TutorialManager.Instance.ShowDialog(dialogAfterCobwebsFirst);
                boxesArrow.SetActive(true);
                player.lockMovement = false;
            }
            else
                yield return TutorialManager.Instance.ShowDialog(dialogAfterCobwebsSecond);
        }

        yield return null;
    }
}
