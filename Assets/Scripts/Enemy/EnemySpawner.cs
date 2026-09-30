using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{ 
    [System.Serializable]
    public class Wave 
    {

        public GameObject enemyPrefab;
        public float spawnTimer; 
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
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(waves[waveNumber].enemyPrefab, waves[waveNumber].parent);

        enemy.transform.localPosition = waves[waveNumber].parent.transform.position;
        
        
    }

}
