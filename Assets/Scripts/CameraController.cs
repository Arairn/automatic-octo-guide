using UnityEngine;
using UnityEngine.Tilemaps;


public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed;
    Vector3 targetCamPosition, currentPosition;
    public Tilemap map;
    float x, y, halfheight, halfWidth;
    Vector3 bottomLeftLimit, topRightLimit;


    // Start is called before the first frame update





    void Start()
    {
        SetTarget(PlayerController.instance.transform);
        Teleport.IsTeleporting += InstantJumpToPlayer;


        InstantJumpToPlayer();


        map = GameObject.FindWithTag("map").GetComponent<Tilemap>();

        bottomLeftLimit = map.localBounds.min;
        topRightLimit = map.localBounds.max;
        halfheight = Camera.main.orthographicSize;
        halfWidth = halfheight * Camera.main.aspect;
        SetMapBoundaries();

    }
    private void OnDestroy() => Teleport.IsTeleporting -= InstantJumpToPlayer;


    private void InstantJumpToPlayer()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetCamPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        currentPosition = transform.position;

        transform.position = Vector3.Lerp(currentPosition, targetCamPosition, Time.deltaTime * cameraSpeed);
        x = Mathf.Clamp(transform.position.x, bottomLeftLimit.x + halfWidth, topRightLimit.x - halfWidth);
        y = Mathf.Clamp(transform.position.y, bottomLeftLimit.y + halfheight, topRightLimit.y - halfheight);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    void SetMapBoundaries()
    {
        PlayerController.instance.SetBounds(map.localBounds.min, map.localBounds.max);
    }


}
