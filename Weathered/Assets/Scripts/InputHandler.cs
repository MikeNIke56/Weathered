using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _camera;
    CharacterCont player;
    private void Awake()
    {
        _camera = Camera.main;
        player = FindAnyObjectByType<CharacterCont>();
    }

    // Update is called once per frame
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        var rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        IsCloseEnough(rayHit);
    }

    void IsCloseEnough(RaycastHit2D obj)
    {
        float xVal = obj.collider.gameObject.transform.position.x - player.transform.position.x;
        float yVal = obj.collider.gameObject.transform.position.y - player.transform.position.y;


        if (Mathf.Abs(xVal) <= player.interactRng && Mathf.Abs(yVal) <= player.interactRng)
            Debug.Log(obj.collider.gameObject.name);

    }
}
