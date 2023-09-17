using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    //Rigidbody's linear drag slows the camera down. followPosLocal is set by the playercontroller. This is setup so if other controls change the player actions it can be changed easily enough. Otherwise, disable this camera and activate other cameras for framing changes/cuts.
    public Vector3 followPosLocal = new Vector3(0f, 0f, -10f);
    [SerializeField]
    float accelerationModifier = 1f;
    [SerializeField]
    Rigidbody2D rbody;
    void Update()
    {
        if (Mathf.Abs((followPosLocal - transform.localPosition).magnitude) < 0.02f)
        {
            transform.localPosition = followPosLocal;
        }
        else
        {
            rbody.AddForce((followPosLocal - transform.localPosition) * accelerationModifier);
        }
    }

    public void SetFollowPosLocal(Vector2 inputVector)
    {
        followPosLocal = new Vector3(inputVector.x, inputVector.y, transform.position.z);
    }
    public void SetFollowPosLocal(Vector3 inputVector)
    {
        followPosLocal = inputVector;
    }
}
