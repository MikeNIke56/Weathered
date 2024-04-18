using UnityEngine;

public class SummoningCircleObj : Interaction
{
    DVDRitual ritual;
    public GameObject[] candles;
    public int candleLitCount = 0;
    [SerializeField] GameObject[] circleStage;

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
            if (candle.activeSelf == false && didLight == false)
            {
                candle.SetActive(true);
                didLight = true;
            }
        }
        circleStage[candleLitCount].SetActive(true);

        for (int i = 0; i < circleStage.Length; i++)
        {
            if (i != candleLitCount)
                circleStage[i].SetActive(false);
        }
    }
    public void ResetCircle()
    {
        foreach (var candle in candles)
            candle.SetActive(false);
        candleLitCount = 0;

        for (int i = 0; i < circleStage.Length; i++)
        {
            if (i != circleStage.Length - 1)
                circleStage[i].SetActive(false);
            else
                circleStage[i].SetActive(true);
        }
    }
}
