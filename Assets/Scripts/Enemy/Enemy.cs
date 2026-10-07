using Unity.AppUI.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private Rigidbody2D rb;
    private Vector3 direction;
    [SerializeField] private float movespeed;
    [SerializeField] private float damage;
    [SerializeField] private float health;
    [SerializeField] private int experienceToGive;
    [SerializeField] private float pushTime;
    [SerializeField] private GameObject destroyEffect;

    private float pushCounter;
    void FixedUpdate()
    {

        if(PlayerMovement.Instance.gameObject.activeSelf)  
        {
            // Face the player
            if(PlayerMovement.Instance.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true; 
            }
            else
            {
                spriteRenderer.flipX = false; 
            }
            if(pushCounter > 0)
            {
                pushCounter -= Time.deltaTime;
                if(movespeed > 0)
                {
                    movespeed = -movespeed;
                }
                if(pushCounter <= 0)
                {
                    movespeed = Mathf.Abs(movespeed);
                }
            }
        
            //Move towards the player
            direction = (PlayerMovement.Instance.transform.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * movespeed, direction.y * movespeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    
    //Make enemy disappear when Player(tag) collision with enemy
    //Checks if the Colliders are touching each other
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement.Instance.TakeDamage(damage);
             //When enemy's destroyd, a copy creates and place it where the enemy was destroyd.
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        DamageNumberController.Instance.CreateNumber(damage, transform.position);
        pushCounter = pushTime;
        if(health <= 0)
        {
            Destroy(gameObject);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            PlayerMovement.Instance.GetExperience(experienceToGive);
        }
    }
}
