using System;
using UnityEngine;

public class AreaAttackPrefab : MonoBehaviour
{
    public AreaAttack attack;
   
    void Start()
    {
        attack = GameObject.Find("Area Attack").GetComponent<AreaAttack>();
        Destroy(gameObject, attack.duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.TakeDamage(1);
        }
    }
}
