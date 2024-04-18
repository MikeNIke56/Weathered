public class TvObj : Interaction
{
    DVDRitual ritual;

    private void Start()
    {
        ritual = FindAnyObjectByType<DVDRitual>();
    }
    public override void onClick()
    {
        ritual.ClickedTV();
    }
}
