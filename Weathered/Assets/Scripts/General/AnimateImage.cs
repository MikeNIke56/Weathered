using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateImage : MonoBehaviour
{
    [SerializeField] Image imageComponent;
    [SerializeField] List<Sprite> frames = new List<Sprite>();
    [SerializeField] float secondsPerFrame = 0.2f;
    public bool stopAnimating = false;

    void Start()
    {
        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>();
        }
        StartAnimating();
    }
    public void StartAnimating()
    {
        StartCoroutine(AnimateThroughFrames());
    }

    IEnumerator AnimateThroughFrames()
    {
        while (!stopAnimating)
        {
            foreach (Sprite frame in frames)
            {
                imageComponent.sprite = frame;
                yield return new WaitForSeconds(secondsPerFrame);
            }
        }
    }
}
