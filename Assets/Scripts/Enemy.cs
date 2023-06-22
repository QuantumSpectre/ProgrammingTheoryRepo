using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool isDetectingEnemy = false;


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
        //bool determined by raycast. So long as it detects ground layer it keeps going
        isAtLedge = !Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);
        //bool determined by racast as well. Detects enemy layer.
        isDetectingEnemy = Physics2D.Raycast(forwardCheck.position, transform.right, 0.2f, enemyLayer);



        //movement 
        if (isFacingRight )
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);

        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        // Flip the enemy's direction if at a ledge or detecting an enemy
        if (isAtLedge || isDetectingEnemy)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            

        }
    }
}
