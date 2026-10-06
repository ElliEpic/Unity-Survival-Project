using UnityEngine;

public class AreaAttack : MonoBehaviour
{
 [SerializeField] private GameObject prefab; 
 private float spawnCounter;
public float cooldown = 5f;
public float duration = 3f;
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0)
        {
            spawnCounter = 5;
            Instantiate(prefab, transform.position, transform.rotation,transform);
        }
    }
}
