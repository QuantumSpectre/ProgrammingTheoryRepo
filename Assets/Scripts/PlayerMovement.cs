using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Properties
    //ENCAPSULATION
    //Gotten publicly by mainmanager but can only be set by this class
    public float health { get; private set; }
    


    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    [SerializeField] private GroundCheck groundCheck;
    private Animator animator;
    private bool isFlipped = false;
    private float h;

    public bool isPaused = false;
    public bool canPause = true;

    // Audio stuff
    public AudioSource aSource;
    public AudioClip jumpClip;
    public bool canJumpSound = true;
    public GameObject deathSoundPrefab;

    private bool isAttacking = false;

    private void Awake()
    {
        health = 1f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Capture input
        float nH = Input.GetAxis("Horizontal");
        // For use in flip()
        h = nH;

        // Movement
        if (nH != 0f)
        {
            if (groundCheck.IsGrounded)
            {
                animator.SetInteger("IsRunning", 1);
            }

            rb.transform.position += new Vector3(nH * moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            animator.SetInteger("IsRunning", 0);
        }

        if (Input.GetButtonDown("Attack") && !isAttacking)
        {
            StartCoroutine(PerformAttack());
        }

        // Flip based on H value
        Flip();

        // Jump
        if (Input.GetButton("Jump") && groundCheck.IsGrounded)
        {
            animator.SetInteger("IsJumping", 1);
            JumpAction();
        }
        else
        {
            animator.SetInteger("IsJumping", 0);
        }

        // Restrict health values
        health = Mathf.Clamp(health, 0f, 1f);

        if (Input.GetButton("Pause") && canPause == true)
        {
            TogglePause();
        }
    }

    private void JumpAction()
    {
        // Only play sound if it's allowed
        if (canJumpSound == true)
        {
            aSource.PlayOneShot(jumpClip);
            canJumpSound = false;
            StartCoroutine(SoundCooldown());
        }

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // Cooldown for the sound that plays when jumping
    IEnumerator SoundCooldown()
    {
        yield return new WaitForSeconds(1);
        canJumpSound = true;
    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(.02f);
        animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            health = health - .25f;
            if (health <= 0f)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        Instantiate(deathSoundPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Flip()
    {
        var nH = h;

        if (nH < 0 && !isFlipped)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            isFlipped = true;
        }

        if (nH > 0 && isFlipped)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            isFlipped = false;
        }
    }

    private void TogglePause()
    {
        if (!canPause) return; // Skip pausing if not allowed

        isPaused = !isPaused;

        if (isPaused)
        {
            // Pause the game
            Time.timeScale = 0f;
           
        }
        else
        {
            // Unpause the game
            Time.timeScale = 1f;
           
        }

        canPause = false;
        StartCoroutine(PauseCooldown());
    }

    IEnumerator PauseCooldown()
    {
        yield return new WaitForSecondsRealtime(.5f);
        canPause = true;
    }
}

