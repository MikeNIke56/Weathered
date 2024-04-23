using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager BGM;
    public float maxVolume = 1f;
    [SerializeField] float fadeStep = 0.05f;
    [SerializeField] List<AudioSource> normalBGMs;
    [SerializeField] List<AudioSource> spiritBGMs;
    [SerializeField] AudioSource DollsSongSource;
    [SerializeField] AudioSource ElevatorSource;
    [SerializeField] AudioSource CelebSource;
    [SerializeField] AudioSource CreepySource;
    public List<Vector3> voidPosition;
    public List<Vector2> voidArea;
    bool isPlaying = true;
    Vector3 voidDifference;
    int currentBGMTrack = 0;
    public AudioSource currentSource;
    bool isFading = false;
    bool isInterupted = false;
    AudioSource interuptSource;
    float interuptFadeSpeed;

    void Start()
    {
        currentSource.volume = 1;
        BGM = this;
        StartCoroutine(CheckVoid());
        StartCoroutine(FadingVolume());
    }
    public void ProgressTrack()
    {
        currentBGMTrack++;
        SwitchWorldBGM(SpiritWorldJump.isInSpiritWorld);
    }
    public void SetTrack(int trackNum)
    {
        currentBGMTrack = trackNum;
        SwitchWorldBGM(SpiritWorldJump.isInSpiritWorld);
    }
    public void SwitchWorldBGM(bool toSpiritWorld)
    {
        float currentTime = currentSource.time;
        if (toSpiritWorld)
        {
            FadeToMusic(spiritBGMs[currentBGMTrack]);
        }
        else
        {
            FadeToMusic(normalBGMs[currentBGMTrack]);
        }
        currentSource.time = currentTime;
    }
    public void AddVoid(Vector3 voidPos, Vector2 voidSize)
    {
        if (voidPosition.Contains(voidPos))
        {
            return;
        }

        voidPosition.Add(voidPos);
        voidArea.Add(voidSize);

        if (voidPosition.Count != voidArea.Count)
        {
            Debug.Log("I can't beleive you've done this. (BGM voids are out of sync)");
        }
    }
    public void RemoveVoid(Vector3 voidPos)
    {
        if (!voidPosition.Contains(voidPos))
        {
            return;
        }

        int voidIndex = voidPosition.IndexOf(voidPos);

        voidPosition.RemoveAt(voidIndex);
        voidArea.RemoveAt(voidIndex);
    }
    IEnumerator FadingVolume()
    {
        bool tempIsPlaying = isPlaying;
        while (true)
        {
            yield return new WaitUntil(() => tempIsPlaying != isPlaying);
            tempIsPlaying = isPlaying;
            do
            {
                if (currentSource == DollsSongSource || isFading)
                {
                    break;
                }
                currentSource.volume += isPlaying ? fadeStep : -fadeStep;
                yield return new WaitForSeconds(0.05f);
            } while (currentSource.volume > 0f && currentSource.volume < maxVolume);
            if (currentSource == DollsSongSource || isFading)
            {
                
            }
            else
            {
                currentSource.volume = isPlaying ? maxVolume : 0f;
            }
        }
    }
    IEnumerator CheckVoid()
    {
        bool tempIsPlaying = false;
        while (true)
        {
            if (maxVolume > 1f)
            {
                maxVolume = 1f;
            }
            tempIsPlaying = true;
            for (int i = 0; i < voidPosition.Count; i++)
            {
                try
                {
                    voidDifference = transform.position - voidPosition[i];
                    if (Mathf.Abs(voidDifference.x) <= voidArea[i].x && Mathf.Abs(voidDifference.y) <= voidArea[i].y)
                    {
                        tempIsPlaying = false;
                    }
                }
                catch
                {
                    Debug.Log("The Void has taken us. (BGM error)");
                }
            }
            if (currentSource == DollsSongSource || isFading)
            {
                isPlaying = true;
            }
            else
            {
                isPlaying = tempIsPlaying;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void FadeToMusic(AudioSource newSource)
    {
        FadeToMusic(newSource, fadeStep);
    }
    public void FadeToMusic(AudioSource newSource, float fadeSpeed)
    {
        if (isFading)
        {
            Debug.Log("Musical grace has been ruined! (Fading was interupted)");
            interuptSource = newSource;
            interuptFadeSpeed = fadeSpeed;
            isInterupted = true;
            isFading = false;
            return;
        }
        if (fadeSpeed == 0f)
        {
            currentSource.volume = 0f;
            currentSource = newSource;
            if (currentSource == DollsSongSource)
            {
                currentSource.volume = 0.5f;
            }
            else
            {
                currentSource.volume = 1f;
            }
        }
        else
        {
            StartCoroutine(FadingMusicCo(newSource, fadeSpeed));
        }
    }

    IEnumerator FadingMusicCo(AudioSource newSource, float fadeSpeed)
    {
        isFading = true;
        float maxDestVol = 1f;
        if (newSource == DollsSongSource)
        {
            maxDestVol = 0.5f;
        }
        while (isFading)
        {
            currentSource.volume = Mathf.Clamp(currentSource.volume - fadeSpeed, 0f, 1f);
            newSource.volume = Mathf.Clamp(newSource.volume + fadeSpeed, 0f, maxDestVol);

            if (currentSource.volume <= 0f && newSource.volume >= maxDestVol)
            {
                isFading = false;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        currentSource.volume = 0f;
        currentSource = newSource;
        currentSource.volume = maxDestVol;
        if (isInterupted)
        {
            isInterupted = false;
            FadeToMusic(interuptSource, interuptFadeSpeed);
        }
    }
    public void FadeDollsSong(float FadeSpeedPos, bool TrueMeansRegularMusic)
    {
        if (TrueMeansRegularMusic)
        {
            FadeToMusic(normalBGMs[currentBGMTrack], FadeSpeedPos);
        }
        else
        {
            FadeToMusic(DollsSongSource, FadeSpeedPos);
        }
    }
}
