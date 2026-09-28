using Unity.AppUI.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private Rigidbody2D rb;
    private Vector3 direction;
    [SerializeField] private float movespeed;

  
    void FixedUpdate()
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
        //Move towards the player
        direction = (PlayerMovement.Instance.transform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * movespeed, direction.y * movespeed);
        
    }
}
