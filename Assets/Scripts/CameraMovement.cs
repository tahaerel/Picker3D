using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool isCameraStop;
    private float cameraMoveSpeed = 4.5f;

    private void Start()
    {
        MovingPlatform.containerStop += StopCameraMovement;
        MovingPlatform.gatesUp += ContinueCameraMovement;
    }

    private void LateUpdate()
    {
        if (!isCameraStop)
        {
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + cameraMoveSpeed * Time.deltaTime);
    }

    private void StopCameraMovement()
    {
        isCameraStop = true;
    }

    private void ContinueCameraMovement()
    {
        isCameraStop = false;
    }

    private void OnDestroy()
    {
        MovingPlatform.containerStop -= StopCameraMovement;
        MovingPlatform.gatesUp -= ContinueCameraMovement;
    }
}
