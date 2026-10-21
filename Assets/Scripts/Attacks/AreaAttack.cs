using Unity.VisualScripting;
using UnityEngine;

public class AreaAttack : Attack
{
 [SerializeField] private GameObject prefab; 
private float spawnCounter;

    public float cooldown;
    public float duration;
    public float damage;
    public float range;
    public float speed;
    public int targetCount; //how many enemies to strike at that upgrade level.
    public float damageBuff = 0;
    public string description;
    public float multiplier = 1;
    void Update()

    {
        spawnCounter -= Time.deltaTime;

        if(spawnCounter <= 0)
        {
            spawnCounter = stats.cooldown;
            Instantiate(prefab, transform.position, transform.rotation,transform);
        }
    }
}
