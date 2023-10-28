using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessStairs : Interaction
{
    public bool isPassable = false;
    bool isDown = true;
    //bool isUp = false;

    public Transform upStairsSpawnPos;
    public Transform downStairsSpawnPos;

    PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    public override void onClick()
    {
        Debug.Log("Clecked Stares");
        if (isPassable == true)
        {
            if(isDown == true)
            {
                player.transform.position = upStairsSpawnPos.position;
                isDown = false;
                //isUp = true;
                Debug.Log("moved upstairs");
            }
            else
            {
                player.transform.position = downStairsSpawnPos.position;
                isDown = true;
                //isUp = false;
                Debug.Log("moved downstairs");
            }
        }
    }
}
