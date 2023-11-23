using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float xSpeed = 0.4f;
    [SerializeField] private float forceSpeed = 2.5f;
    [SerializeField] private float moveForwardSpeed = 4.5f;

    private float lastMousePoint;
    private bool isMouseDown = false;
    private bool isStop = false;
    private Rigidbody rb;
    private List<GameObject> collectedObjects = new List<GameObject>();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        MovingPlatform.containerStop += StopMovement;
        MovingPlatform.gatesUp += ContinueMovement;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spinner"))
        {
            Destroy(other.gameObject);
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if (other.CompareTag("Collectable"))
        {
            collectedObjects.Add(other.gameObject);
            AudioManager.playCollectSound();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            collectedObjects.Remove(other.gameObject);
            AudioManager.playCollectSound();

        }
    }

    private void StopMovement()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        isStop = true;

        foreach (GameObject obj in collectedObjects)
        {
            if (obj != null)
            {
                obj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1f, 1f) * forceSpeed, ForceMode.Impulse);
            }
        }

        collectedObjects.Clear();
    }

    private void ContinueMovement()
    {
        isStop = false;
    }

    private void OnDestroy()
    {
        MovingPlatform.containerStop -= StopMovement;
        MovingPlatform.gatesUp -= ContinueMovement;
    }
}