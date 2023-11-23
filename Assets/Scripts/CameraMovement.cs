using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float cameraMoveSpeed = 4.5f;

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + cameraMoveSpeed * Time.deltaTime);
    }
}
