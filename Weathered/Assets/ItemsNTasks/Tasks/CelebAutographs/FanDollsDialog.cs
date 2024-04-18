using System.Collections;
using UnityEngine;

public class FanDollsDialog : Interaction
{
    [SerializeField] int num;
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    CelebAutoGraphs autoGraphs;
    ArvinLogic arvinLogic;
    [SerializeField] Transform sitPos;
    bool isCompleted = false;

    [SerializeField] GameObject parent;

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
        autoGraphs = FindFirstObjectByType<CelebAutoGraphs>();
        arvinLogic = FindFirstObjectByType<ArvinLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCompleted == false)
        {
            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
    }

    public override void onClick()
    {
        StartCoroutine(TalkToDoll(num));
    }
    IEnumerator TalkToDoll(int num)
    {
        bool doCheckComplete = false;
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        switch (num)
        {
            case 0:
                if (ItemController.itemInHand is ArvinAutograph)
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Doll, "Oh! So cool! Thank you!");
                    doCheckComplete = true;
                }
                else
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Doll, "A sacrifice...");
                break;
            case 1:
                if (ItemController.itemInHand is ArvinAutograph)
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Doll, "It's perfect!");
                    doCheckComplete = true;
                }
                else
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Doll, "Something we want...");
                break;
            case 2:
                if (ItemController.itemInHand is ArvinAutograph)
                {
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Doll, "Yes. That is the name.");
                    doCheckComplete = true;
                }
                else
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Doll, "A name...");
                break;
            default:
                UnityEngine.Debug.Log("fail");
                break;
        }

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();

        if (doCheckComplete)
        {
            Complete();
        }
    }

    public void Complete()
    {
        autoGraphs.completedDolls++;
        autoGraphs.CheckCompleted();
        isCompleted = true;
        transform.position = sitPos.position;
        ItemController.itemInHand = null;
        gameObject.tag = "Untagged";

        foreach (Transform obj in parent.transform)
            Destroy(obj.gameObject);
    }
    public void CompleteSave()
    {
        autoGraphs.completedDolls++;
        isCompleted = true;
        transform.position = sitPos.position;
        ItemController.itemInHand = null;
        gameObject.tag = "Untagged";

        foreach (Transform obj in parent.transform)
            Destroy(obj.gameObject);
    }
}
