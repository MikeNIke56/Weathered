using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteractable = true;
    [SerializeField] Interaction interactionScript;

    public void onClick()
    {
        interactionScript.onClick();
    }
    public void onClickRelease()
    {
        interactionScript.onClickRelease();
    }
    public void onHoverEnter()
    {
        interactionScript.onHoverEnter();
    }
    public void onHoverExit()
    {
        interactionScript.onHoverExit();
    }
}
