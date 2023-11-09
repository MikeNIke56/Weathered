using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TestAudioOptions : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] float newVolume = 0f;
    float currentVolume = 0f;
    // Start is called before the first frame update
    void Start()
    {
        masterMixer.SetFloat("masterVol", newVolume);
    }

    // Update is called once per frame
    void Update()
    {
        if (newVolume != currentVolume)
        {
            newVolume = Mathf.Clamp(newVolume, -80, 20);
            currentVolume = newVolume;
            masterMixer.SetFloat("masterVol", newVolume);
        }
    }
}
