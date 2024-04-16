using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputHandler : MonoBehaviour
{
    public Camera _camera;
    PlayerController player;

    bool taskIsOn = false;

    private void Awake()
    {
        //_camera = Camera.main;
        player = FindAnyObjectByType<PlayerController>();
    }
    private void Start()
    {

    }

    // Update is called once per frame
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (_camera == null || !_camera.isActiveAndEnabled)
        {
            var cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
            foreach(var cam in cameras)
            {
                if(cam.name == "PlayerCamera")
                    _camera = cam;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (!hit.collider) return;
        IsCloseEnough(hit);
    }

    void IsCloseEnough(RaycastHit2D obj)
    {
        GameObject collidedObject = obj.collider.gameObject;

        if(player == null)
            player = FindAnyObjectByType<PlayerController>();

        float xVal = collidedObject.transform.position.x - player.transform.position.x;
        float yVal = collidedObject.transform.position.y - player.transform.position.y;

        Debug.Log("Trying " + collidedObject.name);


        if (Mathf.Abs(xVal) <= player.interactRange && Mathf.Abs(yVal) <= player.interactRange)
        {
            switch (collidedObject.tag)
            {
                case "Interactable":
                    try
                    {
                        if(collidedObject.GetComponent<Interactable>().isActiveAndEnabled == true)
                            collidedObject.GetComponent<Interactable>().onClick();
                    }
                    catch (Exception e)
                    {                    
                        Debug.Log("Failed to interact with " + collidedObject.name);
                        Debug.Log(e);
                    }
                    break;
                case "Obstructor":
                    try
                    {
                        if (collidedObject.transform.parent.gameObject.name == "ArvensPile")
                        {
                            var parentObj = collidedObject.transform.parent.gameObject;
                            parentObj.GetComponent<Interactable>().onClick();
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Failed to interact with " + collidedObject.name);
                        Debug.Log(e);
                    }
                    break;
                default:
                    Debug.Log("Fail");
                    break;
            }
        }
    }

    public void ToggleTaskList()
    {
        if(taskIsOn==false)
        {          
            taskIsOn = true;
            UIController.UIControl.OpenTasksMenu();
        }
        else
        {
            taskIsOn = false;
            UIController.UIControl.CloseTasksMenu();
            UIController.UIControl.closeNotes.Play();
        }
    }
}
