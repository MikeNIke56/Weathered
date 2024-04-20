using UnityEngine;

public class TeaParty : Task
{
    public bool deathTriggered = false;
    bool isTeaPlaced = false;

    //[SerializeField] PlayerController player;

    [SerializeField] Cookies cookies;
    [SerializeField] TeaSet teaSet;
    [SerializeField] Teabags teabags;
    [SerializeField] Kettle kettle;

    [SerializeField] GameObject TeaSetLayer;
    [SerializeField] GameObject WaterLayer;
    [SerializeField] GameObject TeaLayer;
    [SerializeField] GameObject CookiesLayer;

    Pantry pantry;
    TeaCabinet teaCabinet;
    TeabagsCabinet teabagsCabinet;

    bool[] itemsPlaced = { false, false, false, false };

    private void Awake()
    {

    }
    private void Update()
    {
        if (deathTriggered == true && !isFailed)
            OnFailed();
    }

    public override void InstanceTask()
    {
        base.InstanceTask();
        SetTeaparty();
    }

    void SetTeaparty()
    {
        pantry = FindAnyObjectByType<Pantry>();
        teaCabinet = FindAnyObjectByType<TeaCabinet>();
        teabagsCabinet = FindAnyObjectByType<TeabagsCabinet>();

        if (GameManager.GM.hasReloaded == false)
            GameManager.GM.SetTPItems(kettle, cookies, teabags, teaSet);
        else
        {
            kettle = (Kettle)GameManager.GM.GetTPItems()[0];
            cookies = (Cookies)GameManager.GM.GetTPItems()[1];
            teabags = (Teabags)GameManager.GM.GetTPItems()[2];
            teaSet = (TeaSet)GameManager.GM.GetTPItems()[3];
        }
    }

    public void ClickedTable(Interaction interaction)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        if (ItemController.itemInHand == teaSet && isTeaPlaced == false)
        {
            isTeaPlaced = true;
            TeaSetLayer.SetActive(true);
            ItemController.ClearItemInHand();
            CheckOffASpot(0);
        }
        else
        {
            if (isTeaPlaced == false && ItemController.itemInHand != null)
            {
                ShortTextController.STControl.AddShortText("I need plates and cups first.", true);
            }
            else
            {
                switch (ItemController.itemInHand)
                {
                    case Cookies:
                        CookiesLayer.SetActive(true);
                        pantry.canStillInteract = false;
                        ItemController.ClearItemInHand();
                        CheckOffASpot(1);
                        CheckSpots();
                        break;
                    case Kettle:
                        WaterLayer.SetActive(true);
                        ItemController.ClearItemInHand();
                        kettle.kettleObject.GetComponent<BoxCollider2D>().enabled = false;
                        CheckOffASpot(2);
                        CheckSpots();
                        break;
                    case Teabags:
                        TeaLayer.SetActive(true);
                        teabagsCabinet.canStillInteract = false;
                        ItemController.ClearItemInHand();
                        CheckOffASpot(3);
                        CheckSpots();
                        break;
                    default:
                        Debug.Log("failed to place item");
                        break;
                }
            }
        }
    }

    void CheckSpots()
    {
        bool isDone = true;

        foreach (var val in itemsPlaced)
        {
            if (val == false)
                isDone = false;
        }

        if (isDone == true)
        {
            OnCompleted();
            Progression.Prog.FineChinaDolls();
        }
    }

    void CheckOffASpot(int spot)
    {
        itemsPlaced[spot] = true;
    }



    public override void OnFailed()
    {
        isFailed = true;
        //trigger death condition
        Debug.Log("player has died from fall damage (not theirs)");
        GameManager.StartDeath(null, 0f, false);

        GameManager.PC.moveBlockers["CutScene"] = true;
        //ShortTextController.STControl.AddShortText("Oh no, it broke! Aunt will be mad…");
    }

    public override void LoadFinishedTask()
    {
        TeaSetLayer.SetActive(true);
        WaterLayer.SetActive(true);
        TeaLayer.SetActive(true);
        CookiesLayer.SetActive(true);

        teaCabinet = FindAnyObjectByType<TeaCabinet>();
        teaCabinet.LoadCabinet();

        pantry = FindAnyObjectByType<Pantry>();
        pantry.canStillInteract = false;

        teabagsCabinet = FindAnyObjectByType<TeabagsCabinet>();
        teabagsCabinet.canStillInteract = false;

        kettle = FindAnyObjectByType<Kettle>();
        kettle.kettleObject.GetComponent<BoxCollider2D>().enabled = false;

        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
