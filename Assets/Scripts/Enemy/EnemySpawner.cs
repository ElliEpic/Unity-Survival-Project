using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{ 
    [System.Serializable]
    public class Wave 
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

    // Update is called once per frame
    void Update()
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



    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(waves[waveNumber].enemyPrefab, waves[waveNumber].parent);

        enemy.transform.localPosition = waves[waveNumber].parent.transform.position;
         waves[waveNumber].spawnEnemyCount++;
        
        
    }

}
