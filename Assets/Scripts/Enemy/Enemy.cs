using Unity.AppUI.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float knockbackSpeed = 10f;

    private Vector2 knockbackDirection;

    private float normalMoveSpeed;
    private Vector3 direction;
    [SerializeField] private EnemyData enemyData;

    private float movespeed;
    private float damage;
    private float health;
    private int experienceToGive;
    private float pushTime;
    private GameObject destroyEffect;

    private float pushCounter;

    
private void Awake()
{
    if (enemyData == null)
    {
        
        Debug.LogError("Missing EnemyData!", this);
        enabled = false;
        return;
    }

    movespeed = enemyData.moveSpeed;
    normalMoveSpeed = movespeed;
    damage = enemyData.damage;
    health = enemyData.maxHealth;
    experienceToGive = enemyData.experienceToGive;
    pushTime = enemyData.pushTime;
    destroyEffect = enemyData.destroyEffect;

    if (spriteRenderer != null)
        spriteRenderer.sprite = enemyData.sprite;
}
void FixedUpdate()
{
    if (PlayerMovement.Instance == null)
        return;

    if (PlayerMovement.Instance.gameObject.activeSelf)
    {
        // Face the player.
        spriteRenderer.flipX =
            PlayerMovement.Instance.transform.position.x
            > transform.position.x;

        if (pushCounter > 0f)
        {
            // Continue pushing the enemy backwards.
            pushCounter -= Time.fixedDeltaTime;

            rb.linearVelocity = knockbackDirection * knockbackSpeed;
            return;
        }

        // Normal movement resumes when the timer ends.
        direction = (
            PlayerMovement.Instance.transform.position
            - transform.position
        ).normalized;

        rb.linearVelocity = new Vector2(
            direction.x * movespeed,
            direction.y * movespeed
        );
    }
    else
    {
        rb.linearVelocity = Vector2.zero;
    }
}
    //Make enemy disappear when Player(tag) collision with enemy
    //Checks if the Colliders are touching each other


    public void Initialize(float multiplier)
{
    health = enemyData.maxHealth * multiplier;
}


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
{
    PlayerMovement.Instance.TakeDamage(damage);
}


   // PlayerMovement.Instance.TakeDamage(reducedDamage);
            //PlayerMovement.Instance.TakeDamage(damage);
             //When enemy's destroyd, a copy creates and place it where the enemy was destroyd.
        }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        DamageNumberController.Instance.CreateNumber(damage, transform.position);
        pushCounter = pushTime;

        knockbackDirection = (
            transform.position - PlayerMovement.Instance.transform.position
        ).normalized;
        if(health <= 0)
        {
            Destroy(gameObject);
            UIController.Instance.UpdateScore(1);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            PlayerMovement.Instance.GetExperience(experienceToGive);
        }
    }
}
