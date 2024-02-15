using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRange : MonoBehaviour
{
    public bool isFacingMirror = false;
    float distanceBetweenObjects;
    [SerializeField] float minDist;
    PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        distanceBetweenObjects = transform.position.x - player.transform.position.x;

        if(Mathf.Abs(distanceBetweenObjects) <= minDist)
            isFacingMirror = true;
        else
            isFacingMirror = false;
    }
}
