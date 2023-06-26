using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public CircleCollider2D col;

    private MainManager mainManager;


    // Start is called before the first frame update
    void Start()
    {

        //sets necessary declarations
        mainManager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>();
        col = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks for collission's with player, then sets "checkPoint" transform in main manager class
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Checkpoint Saved!");
            mainManager.SetCheckpoint(gameObject.transform);

        }
    }

    
}
