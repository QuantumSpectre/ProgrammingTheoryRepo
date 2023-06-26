using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{

    private float Health { get; set; }
    public float speed = 2f;
    public float knockbackForce;

    private Rigidbody2D rb;
    private BoxCollider2D col;

    public Transform groundCheck;
    public Transform forwardCheck;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    private bool isFacingRight = true;
    private bool isAtLedge = false;
    private bool isFlipping = false;
    private bool isWaiting;

    // Start is called before the first frame update
    void Start()
    {
        
        Health = 100f;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Scanning();

        if (isWaiting == false)
        {
            Movement();
        }

        // Flip the enemy's direction if at a ledge
        if (isAtLedge && !isFlipping)
        {  
        Flip();         
        }
    }

    IEnumerator FlipCoolDown()
    {
        yield return new WaitForSeconds(2);
        isFlipping = false;
    }

    private void Flip()
    {
        isFlipping = true;
        // Switch the direction the enemy is facing
        isFacingRight = !isFacingRight;

        // Flip the enemy's sprite horizontally
        transform.Rotate(0f, 180f, 0f);
        StartCoroutine(FlipCoolDown());
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void JumpForward()
    {
        rb.velocity = new Vector2(rb.velocity.x, 7f);
    }

    private void Scanning()
    {
        //bool determined by raycast. So long as it detects ground layer it keeps going
        isAtLedge = !Physics2D.Raycast(groundCheck.position, Vector2.down, 5f, groundLayer);

        //Detection system
        RaycastHit2D hit = Physics2D.Raycast(forwardCheck.position, transform.right, 1f);

        {
            // Check if the raycast hits a collider
            if (hit.collider != null)
            {
                string tag = hit.collider.gameObject.tag;

                // Use switch statement to handle different tags
                switch (tag)
                {
                    case "Player":
                        // Do something when the tag is "Player"
                        Debug.Log("Player hit!");
                        JumpForward();
                        break;

                    case "Enemy":
                        // Do something when the tag is "Enemy"
                        Debug.Log("Enemy hit!");
                        {
                            int randoValue = Random.Range(1, 4);
                            //Switch case to spice up their actions when running into other enemies
                            //will be great as base class and can change for child classes later
                            switch (randoValue)
                            {
                                case 1:
                                    Debug.Log("Jumped");
                                    JumpForward();
                                    break;

                                case 2:
                                    Debug.Log("Flipped");
                                    Flip();
                                    break;

                                case 3:
                                    Debug.Log("Waiting a bit");
                                    StartCoroutine(Waiting(Random.Range(1, 6)));
                                    break;
                            }
                        }

                        break;

                    case "Ground":
                        // Do something when the tag is "Obstacle"
                        Debug.Log("Ground hit!");
                        JumpForward();
                        break;

                    default:
                        // Handle other tags or do nothing
                        Debug.Log("Unknown tag hit!");
                        break;
                }
            }
        }
    }

    private void Movement()
    {
        //movement can be called when needed
        if (isFacingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);

        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
    
    IEnumerator Waiting(int seconds)
    {   //method for stopping enemies, and then moves them after an input time
        isWaiting = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(seconds);
        Movement();
        isWaiting = false;
    }
}

