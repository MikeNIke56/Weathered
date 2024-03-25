using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, ISavable
{
    public enum GameState {FreeRoam, Menu, ObservationMenu, Death}; //states that the player can be in
    public GameState state;

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

    public bool isPaused = false;

    public Item curItem;

    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject MazarineSpriteObject;
    public bool isFacingRight = true;
    bool lockMovement = false;
    public Dictionary<string, bool> moveBlockers = new Dictionary<string, bool>();

    CelebAutoGraphs sg;

    void Start()
    {
        withinRngIcon.SetActive(false);
        playerCamera.SetFollowPosLocal(playerCameraPos);
        state = GameState.FreeRoam;

        if(GameManager.GM.addedBlockers == false)
        {
            try
            {
                moveBlockers.Add("StepStool", false);
                moveBlockers.Add("TutorialDialog", false);
                moveBlockers.Add("CharacterDialog", false);
                moveBlockers.Add("SpiritWorldTransition", false);
                moveBlockers.Add("CutScene", false);
                moveBlockers.Add("Menu", false);
                GameManager.GM.addedBlockers = true;
            }
            catch (Exception e)
            {
                Debug.Log("already added");
                Debug.Log(e);
            }
        }


        ReloadScene.i.AttachPlayerToReload(this);

        sg = FindAnyObjectByType<CelebAutoGraphs>(FindObjectsInactive.Include);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    sg.LoadFinishedTask();
        //}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIController.UIControl.OpenPauseMenu();
        }

        if (state == GameState.Death)
        {
            return;
        }

        if (cam == null || !cam.isActiveAndEnabled)
        {
            cam = GameObject.FindFirstObjectByType<Camera>();
        }
        lockMovement = false;
        foreach (KeyValuePair<string, bool> isLockedMovement in moveBlockers)
        {
            if (isLockedMovement.Value)
            {
                lockMovement = true;
            }
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

        //temporary method of loading save file
        //if (Input.GetKeyDown(KeyCode.Space)) { SavingSystem.i.Load("SaveSlot"); }

        CursorShow(isPaused);

        if (!lockMovement && Input.GetKeyDown(KeyCode.Space))
        {
            SpiritWorldJump.Jump();
        }

        if (state == GameState.FreeRoam)
        {
            //moveBlockers["Menu"] = false;
        }
        if (state == GameState.Menu)
        {
            moveBlockers["Menu"] = true;     
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

    public void StartDeath()
    {
        state = GameState.Death;
        MazarineSpriteObject.SetActive(false);
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
