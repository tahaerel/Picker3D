using UnityEngine;

public class GateMovement : MonoBehaviour
{
    private float gateSpeed = 70f;
    private float signOfAngle;
    private float angle;

    private bool isGatesUp;
    private bool isThisGate;

    private static int isBothGatesUp;

    void Start()
    {
        MovingPlatform.containerPass += LiftGates;

        CalculateGateRotationAngle();
    }

    void Update()
    {
        OpenGates();

        if(IsGatesFullyOpen())
            GatesUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isThisGate = true;
    }

    private void CalculateGateRotationAngle()
    {
        signOfAngle = Mathf.Sign(transform.localPosition.x);

        if (signOfAngle == 1)
            angle = 60f;
        else
            angle = 300f;
    }

    private void OpenGates()
    {
        bool shouldOpen = isGatesUp && (transform.localRotation.eulerAngles.y * signOfAngle < angle * signOfAngle || transform.localRotation.eulerAngles.y == 0f);

        if (shouldOpen)
            transform.RotateAround(transform.parent.position, Vector3.forward, gateSpeed * signOfAngle * Time.deltaTime);
    }

    private void GatesUp()
    {
        if (IsGatesFullyOpen())
        {
            if (MovingPlatform.gatesUp != null)
            {
                isBothGatesUp++;

                if(isBothGatesUp == 2)
                {
                    isBothGatesUp = 0;
                    MovingPlatform.gatesUp();
                }

                isGatesUp = false;
                isThisGate = false;
            }
        }
    }

    private bool IsGatesFullyOpen()
    {
        bool isFullyOpen = isGatesUp && transform.localRotation.eulerAngles.y * signOfAngle >= angle * signOfAngle;
        return isFullyOpen;
    }

    private void LiftGates()
    {
        if(isThisGate)
            isGatesUp = true;
    }

    private void OnDestroy()
    {
        MovingPlatform.containerPass -= LiftGates;
    }
}
