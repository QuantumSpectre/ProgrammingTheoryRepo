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
        

        if (enemies.Count < maxEnemyCount)
        {
            SpawnEnemy();
        }

        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (GameObject enemyToRemove in enemiesToRemove)
        {
            enemies.Remove(enemyToRemove);
        }
    }

    public void SpawnEnemy()
    {
        enemies.Add(Instantiate(enemyPrefab, spawnPoint.position + new Vector3((Random.Range(2, 9)), 0), Quaternion.identity));
    }
}



