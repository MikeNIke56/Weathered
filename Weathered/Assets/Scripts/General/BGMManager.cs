using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager BGM;
    AudioSource bgmSource;
    public float maxVolume = 1f;
    [SerializeField] float fadeStep = 0.05f;
    [SerializeField] List<AudioClip> normalBGMs;
    [SerializeField] List<AudioClip> spiritBGMs;
    public List<Vector3> voidPosition;
    public List<Vector2> voidArea;
    bool isPlaying = true;
    Vector3 voidDifference;
    int currentBGMTrack = 0;
    [SerializeField] AudioSource DollsSongSource; //TEMP DIALOG CODE

    void Start()
    {
        BGM = this;
        bgmSource = GetComponent<AudioSource>();
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
        float currentTime = bgmSource.time;
        if (toSpiritWorld)
        {
            bgmSource.clip = spiritBGMs[currentBGMTrack];
        }
        else
        {
            bgmSource.clip = normalBGMs[currentBGMTrack];
        }
        bgmSource.time = currentTime;
        bgmSource.Play();
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
                bgmSource.volume += isPlaying ? fadeStep : -fadeStep;
                yield return new WaitForSeconds(0.05f);
            } while (bgmSource.volume > 0f && bgmSource.volume < maxVolume);
            bgmSource.volume = isPlaying ? maxVolume : 0f;
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
            isPlaying = tempIsPlaying;
            yield return new WaitForSeconds(1f);
        }
    }

    public void FadeDollsSong(float FadeSpeedPos, bool TrueMeansRegularMusic) //TEMP DOLLS DIALOG CODE
    {
        if (TrueMeansRegularMusic)
        {
            FindFirstObjectByType<TestAudioOptions>().StartFade(FadeSpeedPos, 0f, -80f);
        }
        else
        {
            FindFirstObjectByType<TestAudioOptions>().StartFade(FadeSpeedPos, -80f, 0f);
        }
    }
}
