using UnityEngine;

public class FixElevator : Task
{
    Fuse fuse;
    [SerializeField] FuseBox fuseBox;

    public enum FuseBoxState { Closed, Open, Fixed };
    public FuseBoxState state;

    [SerializeField] AudioSource fuseboxOpen;
    [SerializeField] AudioSource fuseboxClose;
    [SerializeField] AudioSource fuseboxButton;

    [SerializeField] AudioSource ElevatorMovingSFX;
    [SerializeField] AudioSource TheElevatorJustHellaCrashedYo;

    private void Awake()
    {
        fuse = FindAnyObjectByType<Fuse>();
    }

    public override void InstanceTask()
    {
        base.InstanceTask();
    }
    public void ClickedFuseBox(Interaction interaction)
    {
        if (currentState == taskState.Available)
        {
            OnInProgress();
        }

        if (state == FuseBoxState.Closed)
        {
            state = FuseBoxState.Open;
            interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[0].SetActive(false);
            interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[1].SetActive(true);
            interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[2].SetActive(false);
            fuseboxOpen.Play();

            ShortTextController.STControl.AddShortText("The elevator is broken! I can’t go upstairs!");
            return;
        }
        else if (ItemController.itemInHand is Fuse)
        {
            if (state == FuseBoxState.Open)
            {
                state = FuseBoxState.Fixed;
                interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[0].SetActive(false);
                interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[1].SetActive(false);
                interaction.gameObject.GetComponent<FuseBox>().fuseBoxObjs[2].SetActive(true);
                fuseboxClose.Play();

                ItemController.itemInHand.ClearItem();
                ItemController.itemInHand.gameObject.GetComponent<Fuse>().fuseObject.gameObject.SetActive(false);

                OnCompleted();
            }
        }
    }

    public void ClickedButton()
    {
        if (state == FuseBoxState.Fixed)
        {
            fuseboxButton.Play();
            Debug.Log("taking elevator up");
            GameManager.PC.transform.position += new Vector3(0f, 20f, 0f);
            TheElevatorJustHellaCrashedYo.PlayDelayed(1f);
        }
    }


    public override void OnFailed()
    {
        //trigger death condition
        Debug.Log("player has died");
    }

    public override void LoadFinishedTask()
    {
        state = FuseBoxState.Fixed;

        fuseBox = FindAnyObjectByType<FuseBox>();
        fuseBox.fuseBoxObjs[0].SetActive(false);
        fuseBox.fuseBoxObjs[1].SetActive(false);
        fuseBox.fuseBoxObjs[2].SetActive(true);

        fuse = FindAnyObjectByType<Fuse>();
        fuse.fuseObject.gameObject.SetActive(false);

        currentState = taskState.Completed;
        TaskController.taskControl.CheckCompleteTasks();
    }
}
