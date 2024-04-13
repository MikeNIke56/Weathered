using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer SR;
    [SerializeField]
    float fadeSpeed = 1f;
    bool HasStartedFading = false;
    [SerializeField] bool isTutorial = false;
    [SerializeField] bool isSpiritWorld = false;

    [SerializeField] int room;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !HasStartedFading)
        {
            StartFade();
            DiscoverTasks(room);
        }
    }

    void DiscoverTasks(int room)
    {
        switch (room)
        {
            case 0:
                FindAnyObjectByType<ArrangeDolls>().hasBeenDisc = true;
                FindAnyObjectByType<ReplaceLightBulb>().hasBeenDisc = true;
                FindAnyObjectByType<FixDolls>().hasBeenDisc = true;
                FindAnyObjectByType<MusicBox>().hasBeenDisc = true;
                FindAnyObjectByType<CleanFloor>().hasBeenDisc = true;
                FindAnyObjectByType<ArrangeSnowglobes>().hasBeenDisc = true;
                FindAnyObjectByType<SortToys>().hasBeenDisc = true;
                break;
            case 1:
                FindAnyObjectByType<CleanMirrors>().hasBeenDisc = true;
                FindAnyObjectByType<FixElevator>().hasBeenDisc = true;
                FindAnyObjectByType<TeaParty>().hasBeenDisc = true;
                break;
            case 2:
                FindAnyObjectByType<DVDRitual>().hasBeenDisc = true;
                break;
            case 3:
                FindAnyObjectByType<CelebAutoGraphs>().hasBeenDisc = true;
                break;
            default:
                Debug.Log("Fail");
                break;
        }

        TaskController.taskControl.SetPage(0);
    }

    public void StartFade()
    {
        HasStartedFading = true;
        if (!isTutorial && !isSpiritWorld)
        {
            BGMManager.BGM.ProgressTrack();
        }
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        while (SR.color.a > 0)
        {
            SR.color -= new Color(0f, 0f, 0f, 0.01f * fadeSpeed);
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }
}
