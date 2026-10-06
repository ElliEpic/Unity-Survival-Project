using UnityEngine;

public class AreaAttack : MonoBehaviour
{
 [SerializeField] private GameObject prefab; 
 private float spawnCounter;

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0)
        {
            spawnCounter = 5;
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
