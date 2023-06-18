using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    [SerializeField] private GroundCheck groundCheck;
    private Animator animator;
    private bool isFlipped = false;

    private void Awake()
    {
      rb = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();  
    }

    private void Update()
    {
        //Capture input
        float h = Input.GetAxis("Horizontal");

        //movement
        if (h != 0f)
        {
            if (groundCheck.IsGrounded)
            {
               animator.SetInteger("IsRunning", 1);
            }
           
            rb.transform.position += new Vector3(h * moveSpeed * Time.deltaTime, 0);

        } else { animator.SetInteger("IsRunning", 0);  }

        //flip based on H value
        if (h < 0 && !isFlipped)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            isFlipped = true;
        }

        if (h > 0 && isFlipped) 
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            isFlipped = false;
        }

        //Jump
        if (Input.GetButton("Jump") && groundCheck.IsGrounded)
        {
            animator.SetInteger("IsJumping", 1);
            JumpAction();
        } else { animator.SetInteger("IsJumping", 0); }       

    }

    private void JumpAction()
    {
        
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
    }


}
