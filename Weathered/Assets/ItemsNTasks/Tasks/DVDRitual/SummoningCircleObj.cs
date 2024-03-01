using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SummoningCircleObj : Interaction
{
    DVDRitual ritual;
    public GameObject[] candles;
    public int candleLitCount = 0;

    private void Start()
    {
        ritual = FindAnyObjectByType<DVDRitual>();
    }
    public override void onClick()
    {
        ritual.ClickedSummoningCircle(this);
    }
    public void LightCandle()
    {
        bool didLight = false;

        foreach (var candle in candles)
        {
            if(candle.activeSelf == false && didLight == false)
            {
                candle.SetActive(true);
                didLight = true;
            }
        }
    }
    public void ResetCandles()
    {
        foreach (var candle in candles)
            candle.SetActive(false);
        candleLitCount = 0;
    }
}
