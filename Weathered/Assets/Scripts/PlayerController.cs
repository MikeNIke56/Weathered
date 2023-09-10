using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Feel free to delete this for a more optimized controller, just wanted to test the clicking

    public float moveSpeed = 5f;
    Vector2 movement;
    public Rigidbody2D rb;
    public bool rightBlocked = false; //Obstructors toggle these when colliding begins
    public bool leftBlocked = false;

    private Camera cam;
    private Vector2 mousePos;

    [SerializeField] GameObject withinRngIcon;
    [SerializeField] GameObject outOfRngIcon;

    [SerializeField] public float interactRng;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        withinRngIcon.SetActive(false);
        outOfRngIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        CursorShow();
    }

    void FixedUpdate()
    {
        if (movement.x > 0 && !rightBlocked)
        {
            rb.position += new Vector2(movement.x * moveSpeed * Time.fixedDeltaTime, 0f);
        }
        else if (movement.x < 0 && !leftBlocked)
        {
            rb.position += new Vector2(movement.x * moveSpeed * Time.fixedDeltaTime, 0f);
        }
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void CursorShow()
    {
        withinRngIcon.transform.position = mousePos;
        outOfRngIcon.transform.position = mousePos;

        float xVal = mousePos.x - transform.position.x;
        float yVal = mousePos.y - transform.position.y;


        if (Mathf.Abs(xVal) <= interactRng && Mathf.Abs(yVal) <= interactRng)
        {
            outOfRngIcon.SetActive(false);
            withinRngIcon.SetActive(true);
        }
        else
        {
            withinRngIcon.SetActive(false);
            outOfRngIcon.SetActive(true);
        }
    }

    
}
