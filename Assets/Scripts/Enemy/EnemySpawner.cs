using UnityEditor.Rendering;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnTimer; 
    public float spawnInterval; 
    public Transform parent;

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime; 
        if(spawnTimer >= spawnInterval)
        {
            spawnTimer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, parent);

        enemy.transform.localPosition = parent.transform.position;
        
        
    }

}