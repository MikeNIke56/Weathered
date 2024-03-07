using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    float curFillAmnt;
    public float fillByAmnt;

    [SerializeField] Slider waterBar;
    bool isDone = false;

    void Start()
    {
        curFillAmnt = 0;
    }

    private void OnMouseDrag()
    {
        if(ItemController.itemInHand is WateringCan && isDone == false)
        {
            waterBar.gameObject.SetActive(true);
            curFillAmnt += fillByAmnt;
            waterBar.value = curFillAmnt;
            Mathf.Clamp01(curFillAmnt);

            if (curFillAmnt >= 1)
            {
                Debug.Log("plant is done");
                isDone = true;
            }
        }
        
    }
    private void OnMouseUp()
    {
        waterBar.gameObject.SetActive(false);
    }
}
