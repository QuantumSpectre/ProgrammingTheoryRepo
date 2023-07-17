using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject player;

    //UI Stuffs
    public TextMeshProUGUI coinTextUGUI;
    public Image healthBar;
    public TextMeshProUGUI pauseScreen;

    public int coinCount = 0;

    

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

        
        player = GameObject.FindGameObjectWithTag("Player");



    }

    private void Start()
    {
       
    }


    private void Update()
    {
        //Check for player tagged object, instantiate if none
        if (GameObject.FindWithTag("Player") == null)
        {
            var nPlayer = Instantiate(playerPrefab, checkPoint.position, Quaternion.identity);

            player = nPlayer;
        }

        UpdateHealthBar();

        //pause screen enable
        if (player.GetComponent<PlayerMovement>().isPaused == true) 
        {
            pauseScreen.enabled = true;
        } else { pauseScreen.enabled = false; }

    }

    //Updates the respawn position with checkpoint pos, called by checkpoint gameobjects.
    public void SetCheckpoint(Transform newCheckPoint)
    {
        checkPoint = newCheckPoint;
        
    }

    public void IncreaseCoinCount()
    {
        coinCount++;
        coinTextUGUI.text = "COINS: " + coinCount.ToString();
    }

    public void UpdateHealthBar()
    {
        var playerHealth = player.GetComponent<PlayerMovement>().health;
        healthBar.GetComponent<RectTransform>().localScale = new Vector3(playerHealth, .8f, 1);
       
    }
}
