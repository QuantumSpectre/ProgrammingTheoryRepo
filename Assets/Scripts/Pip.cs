using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pip : Enemy
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hello");
        Scanning();

        if (!isWaiting)
        {
            Movement();
            
        }

        if (isAtLedge && !isFlipping)
        {
            Flip();
        }

        if (!isAtLedge) { Debug.Log("GROUND"); }
    }

    public override void Movement()
    {
        base.Movement();
        
    }

  
}

