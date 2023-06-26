using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{
    [SerializeField]  Enemy parentEnemy;
    [SerializeField] private float bounceForce;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   //simple check to see what collides with head collider. May replace with swtich case later
        Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
        playerRB.velocity = new Vector2(playerRB.velocity.x, bounceForce);
        
        parentEnemy.Death();
    }

}
