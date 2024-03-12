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
    CelebAutoGraphs autoGraphs;
    [SerializeField] int numFlower;
    public GameObject rose;

    [SerializeField] GameObject DryPlant;
    [SerializeField] GameObject WetPlant;

    void Start()
    {
        curFillAmnt = 0;
        autoGraphs = FindAnyObjectByType<CelebAutoGraphs>();
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
                //handle sprite change of flowers
                WetPlant.SetActive(true);
                DryPlant.SetActive(false);
                Debug.Log("plant is done");
                isDone = true;
                autoGraphs.UpdatePlants(numFlower, this);
            }
        }
        
    }
    private void OnMouseUp()
    {
        waterBar.gameObject.SetActive(false);
    }
}
