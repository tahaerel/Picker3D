using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float xSpeed = 0.4f;
    [SerializeField] private float moveForwardSpeed = 4.5f;

    private float lastMousePoint;
    private bool isMouseDown = false;
    public static bool isStop = false;
    private Rigidbody rb;

    private void Start()
    {
        isStop = false;

        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        if (!isStop)
        {
            MovePlayer();
        }
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            lastMousePoint = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
    }

    private void MovePlayer()
    {
        if (isMouseDown)
        {
            float difference = Input.mousePosition.x - lastMousePoint;
            float xPos = Mathf.Clamp(transform.position.x + difference * Time.deltaTime * xSpeed, -1.4f, 1.4f);
            rb.MovePosition(new Vector3(xPos, transform.position.y, transform.position.z + moveForwardSpeed * Time.fixedDeltaTime));
            lastMousePoint = Input.mousePosition.x;
        }
        else
        {
            rb.MovePosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveForwardSpeed * Time.fixedDeltaTime));
        }
    }


}