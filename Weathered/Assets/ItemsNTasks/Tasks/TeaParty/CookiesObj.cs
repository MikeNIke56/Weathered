using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookiesObj : Interaction
{
    [SerializeField] Cookies cookies;

    public override void onClick()
    {
        if (cookies == null)
        {
            cookies = FindFirstObjectByType<Cookies>();
        }
        cookies.ClickedCookiesObject(this);
    }
}
