
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    // Connecting my Unity components to my code
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float movespeed = 2.0f;
    public Vector3 playerMoveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        //Character movement X & Y 
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        playerMoveDirection = new Vector3(inputX, inputY).normalized;

        // Connect my animation sprites to Unity
        animator.SetFloat("moveX", inputX);
        animator.SetFloat("moveY", inputY);


        
        if (playerMoveDirection == Vector3.zero)
        {
            animator.SetBool("moving", false);
        }
        else
        {
            animator.SetBool("moving", true);
        }

        rb.linearVelocity = new Vector3(
            playerMoveDirection.x * movespeed,
            playerMoveDirection.y * movespeed
        );
    }
}