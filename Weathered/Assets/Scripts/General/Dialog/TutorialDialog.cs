using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.TextCore.Text;

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


    public static TutorialDialog i { get; private set; }


    private void Awake()
    {
        i = this;
        sortBoxes = FindFirstObjectByType<SortBoxes>();
        dustCobwebs = FindFirstObjectByType<DustCobwebs>();
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

        //starting dialog
        yield return new WaitForSeconds(.5f);
        yield return TutorialManager.Instance.ShowDialog(introDialog);
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
                yield return TutorialManager.Instance.ShowDialog(dialogAfterBoxFirst);
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
                yield return TutorialManager.Instance.ShowDialog(dialogAfterCobwebsFirst);
            else
                yield return TutorialManager.Instance.ShowDialog(dialogAfterCobwebsSecond);
        }

        yield return null;
    }
}
