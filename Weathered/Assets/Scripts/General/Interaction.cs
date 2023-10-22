using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public virtual void onClick()
    {
        Debug.Log("Clicked " + gameObject.name);
    }
    public virtual void onClickRelease()
    {
        Debug.Log("Released Clicked " + gameObject.name);
    }
    public virtual void onHoverEnter()
    {
        Debug.Log("Hovered " + gameObject.name);
    }
    public virtual void onHoverExit()
    {
        Debug.Log("Unhovered " + gameObject.name);
    }
}
