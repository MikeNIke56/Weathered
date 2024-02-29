using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorCurseFlicker : MonoBehaviour
{
    public Light2D cursedLight;

    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    float time;

    void Start()
    {
        ResetStuff();
        time = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            Flicker();
    }

    void Flicker()
    {
        cursedLight.enabled = true;
        StartCoroutine(ResetStuff());
    }

    IEnumerator ResetStuff()
    {
        yield return Random.Range(minTime, maxTime);
        time = Random.Range(minTime, maxTime);
        cursedLight.enabled = false;
    }
}
