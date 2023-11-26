using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private List<GameObject> collectedObjects = new List<GameObject>();
    [SerializeField] private float forceSpeed = 2.5f;

    void Start()
    {
        MovingPlatform.containerStop += StopMovement;
        MovingPlatform.gatesUp += ContinueMovement;
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
        GameManager.Instance.GameWait();
        transform.GetChild(0).gameObject.SetActive(false);
        PlayerMovement.isStop = true;

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
        GameManager.Instance.GamePlaying();

        PlayerMovement.isStop = false;
    }

    private void OnDestroy()
    {
        MovingPlatform.containerStop -= StopMovement;
        MovingPlatform.gatesUp -= ContinueMovement;
    }
}
