using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{ 
    [System.Serializable] public class Wave 
    {
        public GameObject enemyPrefab; 

        [Tooltip("Time until next spawn")]
        public float spawnTimer; 

        [Tooltip("Set amout of time for the enemy to spawn")]
        public float spawnInterval; 

        public Transform parent;

        public int enemiesPerWave;
        
        public int spawnEnemyCount;
    }

    public List<Wave> waves;
    public int waveNumber;
    public Transform minPos;
    public Transform maxPos;

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.Instance.gameObject.activeSelf)
        {  
            waves[waveNumber].spawnTimer += Time.deltaTime; 
            if(waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
            {
                waves[waveNumber].spawnTimer = 0;
                SpawnEnemy();
            }
            if(waves[waveNumber].spawnEnemyCount >= waves[waveNumber].enemiesPerWave)
            {
                waves[waveNumber].spawnEnemyCount = 0;
                if(waves[waveNumber].spawnInterval > 0.3f)
                {
                    waves[waveNumber].spawnInterval *= 0.9f;
                }
                waveNumber++;
            }
            if(waveNumber >= waves.Count)
            {
                waveNumber = 0;
            }
        }
    }
    
    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate
        (
            waves[waveNumber].enemyPrefab,
            waves[waveNumber].parent
        );
         enemy.transform.localPosition = RandomSpawnPoint(); 
         waves[waveNumber].spawnEnemyCount++;  
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        if(Random.Range(0f, 1f) > 0.5)
        {
            spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
            if(Random.Range(0f, 1f) > 0.5)
            {    
                spawnPoint.y = minPos.position.y;
            }
            else
            {
                spawnPoint.y = maxPos.position.y;
            }
        }
        else
        {
            spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
            if(Random.Range(0f, 1f) > 0.5)
            {    
                spawnPoint.x = minPos.position.x;
            }
            else
            {
                spawnPoint.x = maxPos.position.x;     
            }
        }
    
        return spawnPoint;

    }

}
