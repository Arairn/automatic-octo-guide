using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, x, y;
    Rigidbody2D rb;
    Animator playerAnimator;
    public static PlayerController instance;
    Transform trnsfrm;
    Vector3 bottomLeftLimit, topRightLimit;
    public bool canMove = true;

    public string teleportCameFrom = "";

    public static event Action IsTeleporting;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
        rb = GetComponent<Rigidbody2D>();
        trnsfrm = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove) rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
        else rb.velocity = Vector3.zero;
        if (rb.velocity.SqrMagnitude() >= 0.01f)
        {
            playerAnimator.SetBool("isMoving", true);
            playerAnimator.SetFloat("lastMoveX", rb.velocity.x);
            playerAnimator.SetFloat("lastMoveY", rb.velocity.y);
        }
        else
        {
            playerAnimator.SetBool("isMoving", false);
        }

        playerAnimator.SetFloat("moveX", rb.velocity.x);
        playerAnimator.SetFloat("moveY", rb.velocity.y);

        x = Mathf.Clamp(transform.position.x, bottomLeftLimit.x + 0.5f, topRightLimit.x - 0.5f);
        y = Mathf.Clamp(transform.position.y, bottomLeftLimit.y + 0.8f, topRightLimit.y - 0.8f);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void JumpToPoint(Transform target)
    {
        //Debug.Log("Jumping from"+trnsfrm+"to"+target);
        trnsfrm.position = target.position;
        IsTeleporting?.Invoke();
    }
    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft;
        topRightLimit = topRight;
    }

    public void StopMoving()
    {
        canMove = false;
        IsTeleporting?.Invoke();
    }
    public void StartMoving()
    {
        canMove = true;
    }
}
