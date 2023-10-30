using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputHandler : MonoBehaviour
{
    private Camera _camera;
    PlayerController player;

    [SerializeField] Dialog dialog;

    [SerializeField] GameObject tasksScreen;
    bool taskIsOn = false;

    TaskController taskController;

    private void Awake()
    {
        _camera = Camera.main;
        player = FindAnyObjectByType<PlayerController>();
        taskController = FindAnyObjectByType<TaskController>();
    }

    // Update is called once per frame
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (_camera == null || !_camera.isActiveAndEnabled)
        {
            _camera = GameObject.FindFirstObjectByType<Camera>();
        }
        var rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        IsCloseEnough(rayHit);
    }

    void IsCloseEnough(RaycastHit2D obj)
    {
        GameObject collidedObject = obj.collider.gameObject;

        float xVal = collidedObject.transform.position.x - player.transform.position.x;
        float yVal = collidedObject.transform.position.y - player.transform.position.y;


        if (Mathf.Abs(xVal) <= player.interactRange && Mathf.Abs(yVal) <= player.interactRange)
        {
            switch (collidedObject.tag)
            {
                case "Save":
                    SavingSystem.i.Save("SaveSlot");
                    break;
                case "Task":
                case "Item":
                case "Interactable":
                    //Debug.Log("Trying " + collidedObject.name);
                    try
                    {
                        collidedObject.GetComponent<Interactable>().onClick();
                    }
                    catch (Exception e)
                    {                    
                        Debug.Log("Failed to interact with " + collidedObject.name);
                        Debug.Log(e);
                    }
                    break;
                default:
                    //Debug.Log(collidedObject.name);
                    break;
            }
        }
    }

    public void ToggleTaskList()
    {
        if(taskIsOn==false)
        {          
            tasksScreen.SetActive(true);
            taskIsOn = true;
        }
        else
        {
            tasksScreen.SetActive(false);
            taskIsOn = false;
        }
    }
}
