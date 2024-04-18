using UnityEngine;
using UnityEngine.Audio;

public class TestAudioOptions : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] float newVolume = 0f;
    float currentVolume = 0f;

    float destinationMusicVolume = 0f;
    float currentMusicVolume = 0f;

    float destinationAmbientVolume = -80f;
    float currentAmbientVolume = -80f;

    float fadeStep = 5f;
    bool fadingInSong = false;
    // Start is called before the first frame update
    void Start()
    {
        masterMixer.SetFloat("masterVol", newVolume);
        masterMixer.SetFloat("musicVol", currentMusicVolume);
        masterMixer.SetFloat("ambientVol", currentAmbientVolume);
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

        if (destinationMusicVolume != currentMusicVolume)
        {
            if (fadingInSong)
            {
                currentMusicVolume = Mathf.Clamp(currentMusicVolume - fadeStep * Time.deltaTime, -80, 20);
                if (currentMusicVolume <= destinationMusicVolume)
                {
                    currentMusicVolume = destinationMusicVolume;
                }
            }
            else
            {
                currentMusicVolume = Mathf.Clamp(currentMusicVolume + fadeStep * Time.deltaTime, -80, 20);
                if (currentMusicVolume >= destinationMusicVolume)
                {
                    currentMusicVolume = destinationMusicVolume;
                }
            }
            masterMixer.SetFloat("musicVol", currentMusicVolume);
        }

        if (destinationAmbientVolume != currentAmbientVolume)
        {
            if (fadingInSong)
            {
                currentAmbientVolume = Mathf.Clamp(currentAmbientVolume + fadeStep * Time.deltaTime, -80, 20);
                if (currentAmbientVolume >= destinationAmbientVolume)
                {
                    currentAmbientVolume = destinationAmbientVolume;
                }
            }
            else
            {
                currentAmbientVolume = Mathf.Clamp(currentAmbientVolume - fadeStep * Time.deltaTime, -80, 20);
                if (currentAmbientVolume <= destinationAmbientVolume)
                {
                    currentAmbientVolume = destinationAmbientVolume;
                }
            }
            masterMixer.SetFloat("ambientVol", currentAmbientVolume);
        }
    }
    public void StartFade(float FadeStepB, float MuseDest, float AmbDest)
    {
        fadeStep = FadeStepB;
        destinationMusicVolume = MuseDest;
        destinationAmbientVolume = AmbDest;

        if (currentAmbientVolume < destinationAmbientVolume)
        {
            fadingInSong = true;
        }
        else
        {
            fadingInSong = false;
        }
    }
}
