using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : SelfDestruct
{

    //BASE enemy class others shall derive from
    public float speed = 2f;
    public Transform groundCheck;
    public Transform forwardCheck;
    public LayerMask groundLayer;

    private bool isFacingRight = true;
    private bool isAtLedge = false;
    private bool isFlipping = false;
    private bool isWaiting;

    //Components
    private CircleCollider2D col;
    private Rigidbody2D rb;
    private SpriteRenderer sRenderer;
    public BoxCollider2D headCol;
    

    //coin spawnings
    public GameObject coin;
    public bool canCoin = true;

    //sound stuffs
    public AudioSource aSource;
    public AudioClip aDeathClip;

    private void Start()
    {
        sRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        aSource = GetComponent<AudioSource>();
        
    }
    //top level update behavior gahyuck!
    private void Update()
    {
        Scanning();

        if (!isWaiting)
        {
            Movement();
        }

        if (isAtLedge && !isFlipping)
        {
            Flip();
        }
    }

    //The man the myth the legend, the cool down
    private IEnumerator FlipCoolDown()
    {
        yield return new WaitForSeconds(2);
        isFlipping = false;
    }

    //flip flip flipparoo
    //sets a cooldown and does bool stuffs
    private void Flip()
    {
        isFlipping = true;
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
        StartCoroutine(FlipCoolDown());
    }

    //jumping, also houses groundcheck only called when needing to jump
    private void JumpForward()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(this.transform.position, Vector2.down, .7f, groundLayer);

        if (groundHit.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, 7f);
        }
    }
    //behaviors for forward detection
    private void Scanning()
    {   //ledge detection
        isAtLedge = !Physics2D.Raycast(groundCheck.position, Vector2.down, 5f, groundLayer);
        //forward detection
        RaycastHit2D hit = Physics2D.Raycast(forwardCheck.position, transform.right, 1f);

        if (hit.collider != null)
        {
            string tag = hit.collider.gameObject.tag;

            switch (tag)
            {
                //Response to players
                case "Player":
                    React();
                    break;

                //Response to other enemies
                case "Enemy":
                    React();
                    break;
                //Response to walls
                case "Ground":
                    JumpForward();
                    break;

                default:
                    break;
            }
        }
    }
    //simple movement based on direction
    private void Movement()
    {
        rb.velocity = new Vector2(isFacingRight ? speed : -speed, rb.velocity.y);
    }

    //simple little waiting script
    private IEnumerator Waiting(int seconds)
    {
        isWaiting = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(seconds);
        Movement();
        isWaiting = false;
    }

    //O sweet release
    public void Death()
    {

        StartCoroutine(CoinSpawn());
                                 
        //disabled stuffs to disappear
        col.enabled = false;
        sRenderer.enabled = false;
        //headcol wont just disable so were nuking it :)
        Destroy(headCol);
        aSource.PlayOneShot(aDeathClip);

        StartCoroutine(SlowDeath(2));
    }


    IEnumerator CoinSpawn()
    {
        yield return new WaitForEndOfFrame();
        Instantiate(coin, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_attack"))
        {
            Debug.Log("Sworded");
            Death();
        }
    }

    private void React()
    {
        int randoValue = Random.Range(1, 4);
        switch (randoValue)
        {
            case 1:
                JumpForward();
                break;

            case 2:
                Flip();
                break;

            case 3:
                StartCoroutine(Waiting(Random.Range(1, 6)));
                break;
        }
    }
}
