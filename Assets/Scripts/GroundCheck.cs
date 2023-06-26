using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGrounded {  get; private set; }

    //separate groundcheck object so we can plug into different things
    //this may be unnecessary in the future
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          

    }

    //on collision check if its the ground you are colliding with
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            IsGrounded = true;
        } else { IsGrounded = false; }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {//once you leave that collission, no longer grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }
}
