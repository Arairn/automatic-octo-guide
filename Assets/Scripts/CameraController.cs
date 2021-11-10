using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed;
    Vector3 targetCamPosition;
    // Start is called before the first frame update
    void Start()
    {
        SetTarget(PlayerController.instance.transform);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    4
    // Update is called once per frame
    void LateUpdate()
    {
        targetCamPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, Time.deltaTime*cameraSpeed);
    }
}
