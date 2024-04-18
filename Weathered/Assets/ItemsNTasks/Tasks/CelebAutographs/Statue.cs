using UnityEngine;

public class Statue : Interaction
{
    CelebAutoGraphs autoGraphs;
    [SerializeField] GameObject completedStatue;
    [SerializeField] GameObject head;

    private void Start()
    {
        autoGraphs = FindAnyObjectByType<CelebAutoGraphs>();
    }
    public override void onClick()
    {
        if (autoGraphs == null)
        {
            autoGraphs = FindFirstObjectByType<CelebAutoGraphs>();
        }
        autoGraphs.ClickedStatueObject(this);
    }

    public void CompleteStatue()
    {
        autoGraphs.requirementsMet[2] = true;
        completedStatue.SetActive(true);
        this.gameObject.SetActive(false);
        ItemController.ClearItemInHand();
        head.SetActive(false);
    }
}
