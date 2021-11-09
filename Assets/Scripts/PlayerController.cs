using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    Vector2 moveDirection;
    Rigidbody2D rb;
    Animator playerAnimator;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
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
}
