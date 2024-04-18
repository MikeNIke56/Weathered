public class ElevatorButton : Interaction
{
    FixElevator fixElevator;

    public override void onClick()
    {
        if (fixElevator == null)
        {
            fixElevator = FindFirstObjectByType<FixElevator>();
        }

        fixElevator.ClickedButton();
    }
}
