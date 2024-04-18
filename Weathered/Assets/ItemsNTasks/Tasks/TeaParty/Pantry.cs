using UnityEngine;

public class Pantry : Interaction
{
    [SerializeField] Cookies cookies;
    public bool canStillInteract = true;

    public override void onClick()
    {
        if (cookies == null)
        {
            cookies = FindFirstObjectByType<Cookies>();
        }

        if (canStillInteract == true)
            cookies.ClickedCookiesObject(cookies.cookiesObject);
        else
            ShortTextController.STControl.AddShortText("I don’t need more cookies…");
    }
}
