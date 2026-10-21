using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AreaAttackPrefab : Attack
{
    public AreaAttack areaAttack;
    private Vector3 targetSize;
    private float timer;
    public List<Enemy> enemiesInRange;
    private float counter;
    
    

    void Start()
    {
        areaAttack = GameObject.Find("Area Attack").GetComponent<AreaAttack>();
        //Destroy(gameObject, attack.duration);
        targetSize = Vector3.one * stats.range;
        transform.localScale = Vector3.zero;
        timer = stats.duration;
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
            counter = stats.speed;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                enemiesInRange[i].TakeDamage(stats.damage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.TakeDamage(stats.damage);
           //enemiesInRange.Add(collider.GetComponent<Enemy>());
        }
        
    }

}    
/*    private void OnTriggerEnter2D(Collider2D collider){
      if (collider.CompareTag("Enemy")){
            enemiesInRange.Add(collider.GetComponent<Enemy>());
        }
    } 
    private void OnTriggerExit2D(Collider2D collider){
      if (collider.CompareTag("Enemy")){
            enemiesInRange.Remove(collider.GetComponent<Enemy>());
      }
    }
}
*/
