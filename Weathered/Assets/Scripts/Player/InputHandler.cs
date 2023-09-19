using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _camera;
    PlayerController player;

    [SerializeField] Dialog dialog;

    private void Awake()
    {
        _camera = Camera.main;
        player = FindAnyObjectByType<PlayerController>();
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
        float xVal = obj.collider.gameObject.transform.position.x - player.transform.position.x;
        float yVal = obj.collider.gameObject.transform.position.y - player.transform.position.y;


        if (Mathf.Abs(xVal) <= player.interactRng && Mathf.Abs(yVal) <= player.interactRng)
        {
            switch (obj.collider.gameObject.tag)
            {
                case "Save":
                    SavingSystem.i.Save("SaveSlot");
                    break;
                case "Item":
                    ItemController.i.DisplayItem(obj.collider.gameObject.GetComponent<ItemDetermine>().ChosenItem);
                    break;
                default:
                    Debug.Log(obj.collider.gameObject.name);
                    break;
            }
        }
    }
}
