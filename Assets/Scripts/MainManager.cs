using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [SerializeField] private GameObject playerPrefab;
    

    private Transform checkPoint;

    private void Awake()
    {
        //checks if in scene already, if is then this deletes itself
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //initial position of spawnpoint
        checkPoint = gameObject.transform;
    }

    private void Start()
    {
       
    }


    private void Update()
    {
        //Check for player tagged object, instantiate if none
        if (GameObject.FindWithTag("Player") == null)
        {
            Instantiate(playerPrefab, checkPoint.position, Quaternion.identity);
        }
    }

    //Updates the respawn position with checkpoint pos, called by checkpoint gameobjects.
    public void SetCheckpoint(Transform newCheckPoint)
    {
        checkPoint = newCheckPoint;
        
    }
}
