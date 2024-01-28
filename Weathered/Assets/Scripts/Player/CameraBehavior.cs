using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform playerPos;
    private Vector3 cameraDefaultPos;
    public float speed = .1f;

    [Header("Limit Points")]
    public Transform topLimit;
    public Transform bottomLimit;
    public Transform leftLimit;
    public Transform rightLimit;

    void Start()
    {
        cameraDefaultPos = new Vector3(playerPos.position.x, playerPos.position.y + 3.5f, -10f);
        transform.position = cameraDefaultPos;
    }

    void Update()
    {
        //Look up
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.MoveTowards(transform.position, topLimit.position, speed * Time.deltaTime);
        }
        //Look down
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = Vector3.MoveTowards(transform.position, bottomLimit.position, speed * Time.deltaTime);
        }
        //Look left
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.MoveTowards(transform.position, leftLimit.position, speed * Time.deltaTime);
        }
        //Look right
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.MoveTowards(transform.position, rightLimit.position, speed * Time.deltaTime);
        }
        //Return to default
        else
        {
            cameraDefaultPos = new Vector3(transform.position.x, playerPos.position.y + 3.5f, -10f);
            transform.position = Vector3.MoveTowards(transform.position, cameraDefaultPos, speed * Time.deltaTime);
        }

    }
}
