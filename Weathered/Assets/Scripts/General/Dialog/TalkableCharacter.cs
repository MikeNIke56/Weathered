public class TalkableCharacter : Interaction
{
    PlayerController player;
    public bool isDone = false;

    public override void onClick()
    {

    }

    public static TalkableCharacter i { get; private set; }
    private void Awake()
    {
        i = this;
        player = FindAnyObjectByType<PlayerController>();
    }
}
