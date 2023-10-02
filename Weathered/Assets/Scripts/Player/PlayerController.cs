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
    Vector2 movement;
    public Rigidbody2D rb;
    public bool rightBlocked = false; //Obstructors toggle these when colliding begins
    public bool leftBlocked = false;

    private Camera cam;
    private Vector2 mousePos;

    [SerializeField] GameObject withinRngIcon;

    [SerializeField] public float interactRng;
    [SerializeField] PlayerCameraController playerCamera;
    [SerializeField] Vector2 playerCameraPos = Vector2.zero;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] PauseMenuManager pauseMan;
    public bool isPaused = false;

    void Start()
    {
        withinRngIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null || !cam.isActiveAndEnabled)
        {
            cam = GameObject.FindFirstObjectByType<Camera>();
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space)) { SavingSystem.i.Load("SaveSlot"); }

        CursorShow(isPaused);
        OpenPauseMenu();
    }

    void FixedUpdate()
    {
        if (movement.x > 0 && !rightBlocked)
        {
            rb.position += new Vector2(movement.x * moveSpeed * Time.fixedDeltaTime, 0f);
            if (playerCamera.isActiveAndEnabled)
            {
                playerCamera.SetFollowPosLocal(playerCameraPos);
            }
        }
        else if (movement.x < 0 && !leftBlocked)
        {
            rb.position += new Vector2(movement.x * moveSpeed * Time.fixedDeltaTime, 0f);
            if (playerCamera.isActiveAndEnabled)
            {
                playerCamera.SetFollowPosLocal(new Vector2(-playerCameraPos.x, playerCameraPos.y));
            }
        }
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void CursorShow(bool isPaused=false)
    {
        withinRngIcon.transform.position = mousePos;

        float xVal = mousePos.x - transform.position.x;
        float yVal = mousePos.y - transform.position.y;

        if(isPaused == false)
        {
            if (Mathf.Abs(xVal) <= interactRng && Mathf.Abs(yVal) <= interactRng)
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
            pauseMenu.SetActive(true); 
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
