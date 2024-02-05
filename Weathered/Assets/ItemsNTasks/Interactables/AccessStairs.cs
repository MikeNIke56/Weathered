using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessStairs : Interaction
{
    public bool isPassable = false;
    bool isAboutToBreak = true;
    bool isBroken = false;

    [SerializeField] Transform upStairsSpawnPos;
    [SerializeField] Transform downStairsSpawnPos;
    [SerializeField] Transform stairsFallSpotPos;

    PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    public override void onClick()
    {
        if (isAboutToBreak && isPassable)
        {
            player.transform.position = stairsFallSpotPos.position;
        }
        else if (isBroken)
        {

        }
        else if (isPassable && !isBroken)
        {
            if(player.transform.position.y <= downStairsSpawnPos.position.y)
            {
                player.transform.position = upStairsSpawnPos.position;
            }
            else
            {
                player.transform.position = downStairsSpawnPos.position;
            }
        }
    }
}
