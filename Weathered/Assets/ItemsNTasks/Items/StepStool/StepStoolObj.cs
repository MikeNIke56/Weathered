using UnityEngine;

public class StepStoolObj : Interaction
{
    StepStool stepStool;
    public Transform stepUpPos;
    public Transform stepDownPos;
    public bool onStool = false;

    [SerializeField] ReplaceLightBulb replaceLightTask;
    PlayerController player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        replaceLightTask = FindAnyObjectByType<ReplaceLightBulb>();
        stepStool = FindAnyObjectByType<StepStool>();
    }
    public override void onClick()
    {
        if (ItemController.itemInHand != stepStool && replaceLightTask.stoolPlaced == false)
        {
            ItemController.AddItemToHand(stepStool);
            gameObject.SetActive(false);
        }
        else if (replaceLightTask.stoolPlaced == true)
        {
            if (onStool == false)
            {
                stepDownPos.position = player.transform.position;
                player.transform.position = stepUpPos.position;
                onStool = true;
                player.moveBlockers["StepStool"] = true;
            }
            else
            {
                player.transform.position = stepDownPos.position;
                onStool = false;
                player.moveBlockers["StepStool"] = false;
            }
        }
    }
}
