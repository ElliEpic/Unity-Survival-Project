using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class ThunderboltAttack : Attack
{
    [SerializeField] private GameObject prefab; 
    public AreaAttack attack;
    private Vector3 targetSize;
    private float timer;
    public List<Enemy> enemiesInRange;
    private float counter;
   
    void Start()
    {
        attack = GameObject.Find("Thunderbolt Attack").GetComponent<AreaAttack>();
        //Destroy(gameObject, attack.duration);
        targetSize = Vector3.one * attack.stats[attack.attackLevel].range;
        transform.localScale = Vector3.zero;
        timer = attack.stats[attack.attackLevel].duration;
    }

    void Update()
    {


        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * 7);
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            targetSize = Vector3.zero;
            if(transform.localScale.x == 0f)
            {
                Destroy(gameObject);
            }
        }
        counter -= Time.deltaTime;
        if(counter <= 0)
        {
            counter = attack.stats[attack.attackLevel].speed;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                enemiesInRange[i].TakeDamage(attack.stats[attack.attackLevel].damage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.TakeDamage(attack.stats[attack.attackLevel].damage);
           //enemiesInRange.Add(collider.GetComponent<Enemy>());
        }
        
    }
}
