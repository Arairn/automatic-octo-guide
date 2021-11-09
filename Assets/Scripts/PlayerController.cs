using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;
    Animator playerAnimator;
    public static PlayerController instance;
    Transform trnsfrm;

    public string teleportCameFrom ="";


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

        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        trnsfrm = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized*moveSpeed;
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
               
    }

    public void JumpToPoint(Transform target)
    {
        Debug.Log("Jumping from"+trnsfrm+"to"+target);
        trnsfrm.position = target.position;
    }
}
