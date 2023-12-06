using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneControl : Interaction
{
    [SerializeField] AudioSource ringingSFX;
    [SerializeField] SpriteRenderer phoneSR;
    [SerializeField] Sprite phoneOff;
    [SerializeField] Sprite phoneLight;
    bool isAnswered = false;
    [SerializeField] float flashSpeed = 1f;
    bool isOffSprite = true;
    bool isAnswerable = false;

    public void NewVoicemail()
    {
        isAnswerable = true;
        ringingSFX.Play();
        StartCoroutine(PendingVoicemail());
    }
    public IEnumerator PendingVoicemail()
    {
        while (!isAnswered)
        {
            if (isOffSprite)
            {
                phoneSR.sprite = phoneLight;
                isOffSprite = false;
            }
            else
            {
                phoneSR.sprite = phoneOff;
                isOffSprite = true;
            }
            yield return new WaitForSeconds(flashSpeed);
        }
        phoneSR.sprite = phoneLight;
    }

    public override void onClick()
    {
        if (isAnswerable)
        {
            UIController.UIControl.OpenAuntVoicemail();
            isAnswered = true;
        }
    }
}
