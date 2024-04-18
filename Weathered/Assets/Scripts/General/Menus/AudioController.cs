using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeGroup;

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat(volumeGroup, Mathf.Log10(sliderValue) * 20);
    }
}
