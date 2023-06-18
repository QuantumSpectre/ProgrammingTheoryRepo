using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float Health { get;  set; }
    public float speed = 2f;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isFacingRight = true;
    private bool isAtLedge = false;



    // Start is called before the first frame update
    void Start()
    {
        Health = 100f;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        isAtLedge = !Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);

        

        //movement 
        if (isFacingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        // Flip the enemy's direction if at a ledge
        if (isAtLedge)
        {
            Flip();
        }


    }

    private void Flip()
    {
        // Switch the direction the enemy is facing
        isFacingRight = !isFacingRight;

        // Flip the enemy's sprite horizontally
        transform.Rotate(0f, 180f, 0f);
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    
}
