using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxBox : Interaction
{
    [SerializeField] MusicBox musicTask;
    public Item currentCassette;
    AudioSource currentAudio;
    [SerializeField] List<Item> cassetteTapes;
    [SerializeField] List<AudioSource> tapeMusic;
    [SerializeField] AudioSource tapeJammed;
    [SerializeField] AudioSource tapeInserted;
    [SerializeField] AudioSource buttonSFX;
    public bool isSpiritBox = false;
    bool isInserting = false;

    void Start()
    {
        if (currentCassette != null)
        {
            InsertTape(currentCassette);
        }      
    }
    public override void onClick()
    {
        if (musicTask == null)
        {
            musicTask = FindFirstObjectByType<MusicBox>();
        }
        musicTask.ClickedBox(this);
    }

    public void RetreiveTape()
    {
        if (currentAudio != null)
        {
            currentAudio.Stop();
            currentAudio = null;
        }
        if (currentCassette != null)
        {
            ItemController.AddItemToHand(currentCassette);
            currentCassette = null;
        }
        BGMManager.BGM.RemoveVoid(transform.position);
        //Change sprite to empty?
    }

    public void InsertTape(Item insertedTape)
    {
        if (!isInserting)
        {
            isInserting = true;
            currentCassette = insertedTape;
            currentAudio = tapeMusic[cassetteTapes.IndexOf(insertedTape)];
            ItemController.ClearItemInHand();
            BGMManager.BGM.AddVoid(transform.position, new Vector2(18, 5));
            StartCoroutine(TapeInsertTimer());
        }
        else
        {
            tapeJammed.Play();
        }
    }

    IEnumerator TapeInsertTimer()
    {
        tapeInserted.Play();
        yield return new WaitForSeconds(1f);
        buttonSFX.Play();
        yield return new WaitForSeconds(0.5f);
        currentAudio.Play();
        yield return new WaitForSeconds(0.5f);
        isInserting = false;
    }


}
