using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessStairs : Interaction
{
    public bool isPassable = false;
    bool isAboutToBreak = true;

    [SerializeField] Transform upStairsSpawnPos;
    [SerializeField] Transform downStairsSpawnPos;
    [SerializeField] Transform stairsFallSpotPos;

    [SerializeField] AudioSource StairsSFX;
    [SerializeField] AudioSource StairsBreakSFX;

    [SerializeField] GameObject FixedStairs;
    [SerializeField] GameObject BrokenStairs;

    PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    public void HandleStairsLoad(bool areStairsBroken)
    {
        if (isPassable)
        {
            if (areStairsBroken == false)
            {
                isAboutToBreak = false;
                FixedStairs.SetActive(false);
                BrokenStairs.SetActive(true);
            }
            else
            {
                FixedStairs.SetActive(true);
                BrokenStairs.SetActive(false);
            }
        }
    }

    public override void onClick()
    {
        if (isPassable)
        {
            if (!Progression.Prog.HasFixedStairs && isAboutToBreak)
            {
                player.transform.position = stairsFallSpotPos.position;
                isAboutToBreak = false;
                StairsBreakSFX.Play();
                FixedStairs.SetActive(false);
                BrokenStairs.SetActive(true);
            }
            else if (!Progression.Prog.HasFixedStairs)
            {
                ShortTextController.STControl.AddShortText("The stairs are broken. I'd just fall again...", true);
            }
            else
            {
                FixedStairs.SetActive(true);
                BrokenStairs.SetActive(false);

                if (player.transform.position.y <= downStairsSpawnPos.position.y)
                {
                    player.transform.position = upStairsSpawnPos.position;
                }
                else
                {
                    player.transform.position = downStairsSpawnPos.position;
                }

                StairsSFX.Play();
            }
        }
    }

    public bool IsAboutToBreak => isAboutToBreak;
}
