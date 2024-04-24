using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static GameManager;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeGroup;
    private float volume;

    public void SetVolume(float sliderValue)
    {
        var volume = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat(volumeGroup, volume);
        this.volume = volume;
    }

    public void SetLoadSound(object state, int pos)
    {
        var saveData = state as GameManagerSaveData;
        switch (pos)
        {
            case 0:
                this.volumeGroup = saveData.volumeGroupMusic;
                this.volume = saveData.volumeMusic;
                break;
            case 1:
                this.volumeGroup = saveData.volumeGroupMaster;
                this.volume = saveData.volumeMaster;
                break;
            case 2:
                this.volumeGroup = saveData.volumeGroupSFX;
                this.volume = saveData.volumeSFX;
                break;
            default:
                Debug.Log("Fail");
                break;
        }

        SetVolume(this.volume);
    }
}

