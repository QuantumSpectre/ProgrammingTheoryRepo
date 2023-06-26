using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int maxEnemyCount = 3;

    public List<GameObject> enemies = new List<GameObject>();

    private void FixedUpdate()
    {
        
        //spawn enemies if below max count
        if (enemies.Count < maxEnemyCount)
        {
            SpawnEnemy();
        }
        //list to manage enemies and their count
        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                enemiesToRemove.Add(enemy);
            }
        }
        //seperate list to manage enemies that should be removed
        //two lists because a list cannot be edited on the same iteration that it's declared
        //so a seperate list will edit the initial list on the second iteration
        foreach (GameObject enemyToRemove in enemiesToRemove)
        {
            enemies.Remove(enemyToRemove);
        }
    }

    public void SpawnEnemy()
    {   //simple instantiate within a small range
        enemies.Add(Instantiate(enemyPrefab, spawnPoint.position + new Vector3((Random.Range(2, 9)), 0), Quaternion.identity));
    }
}



