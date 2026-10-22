using Unity.VisualScripting;
using UnityEngine;

public class AreaAttack : Attack
{
 [SerializeField] private GameObject prefab; 
private float spawnCounter;

    void Update()

    {
        spawnCounter -= Time.deltaTime;

        if(spawnCounter <= 0)
        {
            spawnCounter = stats[attackLevel].cooldown;
            Instantiate(prefab, transform.position, transform.rotation,transform);
        }
    }
}
