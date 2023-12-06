using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShelfScrolling : MonoBehaviour
{
    bool isUp = true;
    [SerializeField] public GameObject upUI;
    [SerializeField] public GameObject downUI;

    [SerializeField] GameObject sgUI;
    [SerializeField] Transform upPos;
    [SerializeField] Transform downPos;

    private void Start()
    {
        Deactivate();
    }

    private void Update()
    {
        if (!isUp)
        {
            downUI.SetActive(false);
            upUI.SetActive(true);
        }
        else
        {
            upUI.SetActive(false);
            downUI.SetActive(true);
        }
    }

    public void ScrollUp()
    {
        if(!isUp)
        {
            isUp = true;
            sgUI.transform.localPosition = upPos.localPosition;
        }
    }
    public void ScrollDown()
    {
        if(isUp)
        {
            isUp = false;
            sgUI.transform.localPosition = downPos.localPosition;
        }
    }

    public void Deactivate()
    {
        upUI.SetActive(false);
        downUI.SetActive(false);
    }
}
