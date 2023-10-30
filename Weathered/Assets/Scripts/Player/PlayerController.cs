using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, ISavable
{
    public enum GameState {FreeRoam, Menu, ObservationMenu, Death}; //states that the player can be in

    //Feel free to delete this for a more optimized controller, just wanted to test the clicking

    public float moveSpeed = 5f;
    Vector2 movement = Vector2.zero;
    Vector2 currentMovement = Vector2.zero;
    public Rigidbody2D rb;
    public bool rightBlocked = false; //Obstructors toggle these when colliding begins
    public bool leftBlocked = false;
    public bool forcedMovement = false;

    private Camera cam;
    private Vector2 mousePos;

    [SerializeField] GameObject withinRngIcon;

    public float interactRange;

    [SerializeField] PlayerCameraController playerCamera;
    [SerializeField] Vector2 playerCameraPos = new Vector2(2f, 3f);
    [SerializeField] float playerCameraUpY = 5f;
    [SerializeField] float playerCameraDownY = 1f;

    [SerializeField] PauseMenuManager pauseMan;
    public bool isPaused = false;

    public Item curItem;

    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject MazarineSpriteObject;
    public bool isFacingRight = true;
    public bool lockMovement = false;

    void Start()
    {
        withinRngIcon.SetActive(false);
        playerCamera.SetFollowPosLocal(playerCameraPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null || !cam.isActiveAndEnabled)
        {
            cam = GameObject.FindFirstObjectByType<Camera>();
        }
        if (lockMovement)
        {
            movement = Vector2.zero;
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space)) { SavingSystem.i.Load("SaveSlot"); }

        CursorShow(isPaused);
        OpenPauseMenu();

        if (Input.GetKeyDown(KeyCode.P))
        {
            SpiritWorldJump.Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIController.UIControl.CloseInteractionMenu();
        }
    }

    void FixedUpdate()
    {
        if (movement.x > 0 && currentMovement.x != 1f)
        {
            currentMovement.x = 1f;
            OnMoveXPos();
        }
        else if (movement.x < 0 && currentMovement.x != -1f)
        {
            currentMovement.x = -1f;
            OnMoveXNeg();
        }
        else if (movement.x == 0 && currentMovement.x != 0f)
        {
            currentMovement.x = 0f;
            OnMoveXZero();
        }
        if (movement.y > 0 && currentMovement.y != 1f)
        {
            currentMovement.y = 1f;
            OnMoveYPos();
        }
        else if (movement.y < 0 && currentMovement.y != -1f)
        {
            currentMovement.y = -1f;
            OnMoveYNeg();
        }
        else if (movement.y == 0 && currentMovement.y != 0f)
        {
            currentMovement.y = 0f;
            OnMoveYZero();
        }

        if (!forcedMovement && rightBlocked && rb.velocity.x > 0)
        {
            playerAnimator.SetBool("isWalking", false);
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else if (!forcedMovement && leftBlocked && rb.velocity.x < 0)
        {
            playerAnimator.SetBool("isWalking", false);
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

    }
    void OnMoveXPos()
    {
        isFacingRight = true;
        MazarineSpriteObject.transform.localScale = new Vector3(MathF.Abs(MazarineSpriteObject.transform.localScale.x), MazarineSpriteObject.transform.localScale.y, MazarineSpriteObject.transform.localScale.z);
        if (!rightBlocked && !forcedMovement)
        {
            playerAnimator.SetBool("isWalking", true);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

        if (playerCamera.isActiveAndEnabled)
        {
            playerCamera.SetFollowXLocal(playerCameraPos.x);
        }
    }
    void OnMoveXZero()
    {
        if (!forcedMovement)
        {
            playerAnimator.SetBool("isWalking", false);
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
    void OnMoveXNeg()
    {
        isFacingRight = false;
        MazarineSpriteObject.transform.localScale = new Vector3(-MathF.Abs(MazarineSpriteObject.transform.localScale.x), MazarineSpriteObject.transform.localScale.y, MazarineSpriteObject.transform.localScale.z);
        if (!leftBlocked && !forcedMovement)
        {
            playerAnimator.SetBool("isWalking", true);
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }

        if (playerCamera.isActiveAndEnabled)
        {
            playerCamera.SetFollowXLocal(-playerCameraPos.x);
        }
    }
    void OnMoveYPos()
    {
        if (playerCamera.isActiveAndEnabled)
        {
            playerCamera.SetFollowYLocal(playerCameraUpY);
        }
    }
    void OnMoveYZero()
    {
        if (playerCamera.isActiveAndEnabled)
        {
            playerCamera.SetFollowYLocal(playerCameraPos.y);
        }
    }
    void OnMoveYNeg()
    {
        if (playerCamera.isActiveAndEnabled)
        {
            playerCamera.SetFollowYLocal(playerCameraDownY);
        }
    }

    void CursorShow(bool isPaused=false)
    {
        withinRngIcon.transform.position = mousePos;

        float xVal = mousePos.x - transform.position.x;
        float yVal = mousePos.y - transform.position.y;

        if(isPaused == false)
        {
            if (Mathf.Abs(xVal) <= interactRange && Mathf.Abs(yVal) <= interactRange)
            {
                withinRngIcon.SetActive(true);
            }
            else
            {
                withinRngIcon.SetActive(false);
            }
        }
        else
            withinRngIcon.SetActive(false);
    }

    void OpenPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPaused == false) 
        { 
            pauseMan.gameObject.SetActive(true); 
            Time.timeScale = 0f;
            isPaused = true;
        }
    }



    public object CaptureState()
    {
        var saveData = new PlayerSaveData()
        {
            position = new float[] { transform.position.x, transform.position.y },
        };

        return saveData;
    }

    public void RestoreState(object state)
    {
        var saveData = (PlayerSaveData)state;
        var pos = saveData.position;
        transform.position = new Vector3(pos[0], pos[1]);
    }
    [Serializable]
    public class PlayerSaveData
    {
        public float[] position;
    }


}
