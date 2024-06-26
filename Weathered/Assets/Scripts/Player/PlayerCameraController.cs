using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    //Rigidbody's linear drag slows the camera down. followPosLocal is set by the playercontroller.
    //This is setup so if other controls change the player actions it can be changed easily enough. Otherwise, disable this camera and activate other cameras for framing changes/cuts.
    public Vector3 followPosLocal = new Vector3(0f, 0f, -10f);
    [SerializeField]
    float accelerationModifier = 1f;
    [SerializeField]
    Rigidbody2D rbody;
    PlayerController playerController;
    bool hasReachedPoint = false;
    //FIX IT BY SEPERATING AXIS THEN JUST HAVE THEM CHECK
    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }
    void Update()
    {
        if (playerController.isPaused)
        {
            return;
        }

        if (playerController.isFacingRight && transform.localPosition.x > followPosLocal.x)
        {
            transform.localPosition = followPosLocal;
        }
        else if (!playerController.isFacingRight && transform.localPosition.x < followPosLocal.x)
        {
            transform.localPosition = followPosLocal;
        }
        if (hasReachedPoint && Mathf.Abs((followPosLocal - transform.localPosition).magnitude) < 1f)
        {
            transform.localPosition = followPosLocal;
        }
        else if (Mathf.Abs((followPosLocal - transform.localPosition).magnitude) < 0.02f)
        {
            hasReachedPoint = true;
            transform.localPosition = followPosLocal;
        }
        else
        {
            hasReachedPoint = false;
            rbody.AddForce((followPosLocal - transform.localPosition) * accelerationModifier);
        }
    }

    public void SetFollowPosLocal(Vector2 inputVector)
    {
        followPosLocal = new Vector3(inputVector.x, inputVector.y, followPosLocal.z);
    }
    public void SetFollowPosLocal(Vector3 inputVector)
    {
        followPosLocal = inputVector;
    }
    public void SetFollowXLocal(float inputX)
    {
        followPosLocal = new Vector3(inputX, followPosLocal.y, followPosLocal.z);
    }
    public void SetFollowYLocal(float inputY)
    {
        followPosLocal = new Vector3(followPosLocal.x, inputY, followPosLocal.z);
    }
}
