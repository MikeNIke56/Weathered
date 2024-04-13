using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform playerPos;
    private Vector3 cameraDefaultPos;
    public float speed = 10f;

    [Header("Limit Points")]
    public Transform topLimit;
    public Transform bottomLimit;
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform topLeftLimit;
    public Transform topRightLimit;
    public Transform bottomLeftLimit;
    public Transform bottomRightLimit;

    void Start()
    {
        cameraDefaultPos = new Vector3(playerPos.position.x + 2f, playerPos.position.y + 3.5f, -10f);
        transform.position = cameraDefaultPos;
    }

    void Update()
    {
        if(UIController.UIControl.isCamFree == true)
        {
            //Look right
            if (Input.GetKey(KeyCode.D))
            {
                //Right up
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position = Vector3.MoveTowards(transform.position, topRightLimit.position, speed * Time.deltaTime);
                }
                //Right down
                else if (Input.GetKey(KeyCode.S))
                {
                    transform.position = Vector3.MoveTowards(transform.position, bottomRightLimit.position, speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, rightLimit.position, speed * Time.deltaTime);
                }
            }
            //Look left
            else if (Input.GetKey(KeyCode.A))
            {
                //Left up
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position = Vector3.MoveTowards(transform.position, topLeftLimit.position, speed * Time.deltaTime);
                }
                //Left down
                else if (Input.GetKey(KeyCode.S))
                {
                    transform.position = Vector3.MoveTowards(transform.position, bottomLeftLimit.position, speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, leftLimit.position, speed * Time.deltaTime);
                }
            }
            //Look up
            else if (Input.GetKey(KeyCode.W))
            {
                //Left up
                if (Input.GetKey(KeyCode.A) || transform.position.x < playerPos.position.x)
                {
                    transform.position = Vector3.MoveTowards(transform.position, topLeftLimit.position, speed * Time.deltaTime);
                }
                //Right up
                else if (Input.GetKey(KeyCode.D) || transform.position.x > playerPos.position.x)
                {
                    transform.position = Vector3.MoveTowards(transform.position, topRightLimit.position, speed * Time.deltaTime);
                }
                //up
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, topLimit.position, speed * Time.deltaTime);
                }
            }
            //Look down
            else if (Input.GetKey(KeyCode.S))
            {
                //Left down
                if (Input.GetKey(KeyCode.A) || transform.position.x < playerPos.position.x)
                {
                    transform.position = Vector3.MoveTowards(transform.position, bottomLeftLimit.position, speed * Time.deltaTime);
                }
                //Right down
                else if (Input.GetKey(KeyCode.D) || transform.position.x > playerPos.position.x)
                {
                    transform.position = Vector3.MoveTowards(transform.position, bottomRightLimit.position, speed * Time.deltaTime);
                }
                //down
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, bottomLimit.position, speed * Time.deltaTime);
                }
            }
            //Return to default
            else
            {
                cameraDefaultPos = new Vector3(transform.position.x, playerPos.position.y + 3.5f, -10f);
                transform.position = Vector3.MoveTowards(transform.position, cameraDefaultPos, speed * Time.deltaTime);
            }
        }
        

    }
}
