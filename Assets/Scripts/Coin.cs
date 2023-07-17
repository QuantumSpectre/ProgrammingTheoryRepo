using System;
using System.Collections;
using UnityEngine;

public class Coin : Item
{

    public Rigidbody2D rb;
    public CircleCollider2D col;
    public MainManager mManager;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public SpriteRenderer sRenderer;
    public Animator animator;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mManager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>();
        audioSource = GetComponent<AudioSource>();
        sRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(Flicker());
    }

    void Update()
    {
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collide with player, increase coincount in mainmanager and destroy self
        //ADD SOUND FX
        if (collision.CompareTag("Player"))
        {
            //play sound
            audioSource.PlayOneShot(audioClip);

            //make disappear
            sRenderer.enabled = false;
            col.enabled = false;

            //increase coin count
            mManager.IncreaseCoinCount();
            //wait to allow the sound to play
            StartCoroutine(SlowDeath(2));
        }


        
       

    }

    //We make the coin appear to be gone by turning off the sprite renderer and the collider 
    //in reality the 'coin' is still there which allows the sound to play out without appearing
    //within the scene
    IEnumerator SlowDeath(int timeForDeath)
    {
        yield return new WaitForSeconds(timeForDeath);
        Destroy(this.gameObject);
    }

    //causes flicker, then asks for death
    IEnumerator Flicker()
    {
       
        yield return new WaitForSeconds(4);

        animator.SetBool("StartFlickering", true);

        StartCoroutine(SlowDeath(3));
    }
}
