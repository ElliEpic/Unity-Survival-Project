using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    // Connecting my Unity components to my code even w1hen it's private
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float movespeed = 2.0f;
    [SerializeField] private Camera mainCamera;
    public Vector3 playerMoveDirection;
    public float playerMaxHealth;
    public float playerHealth;

    public int experience;
    public int currentevel;
    public int maxLevel;
    public List<int> playerLevels;

    public Attack activeAttack;
// invisibillity frames after damage
    private bool isImmune;
    [SerializeField] private float immunityDuration;
    [SerializeField] private float immunityTimer;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Collider2D playerCollider;
    
    public int damageBonus = 3;

    [SerializeField] private float blinkInterval = 0.15f;

    private float blinkTimer;
    private bool isVisible = true;

    /* void Start() 
     { 
         rb = GetComponent<Rigidbody2D>(); 
     }*/

    void Start()
    {
        for(int i = playerLevels.Count; i < maxLevel; i++)
        {
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count - 1] * 1.1f + 15));
        }
        playerHealth = playerMaxHealth;
        UIController.Instance.UpdateHealthSlider();
        UIController.Instance.UpdateExperienceSlider();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

   
void Update()
{
    float inputX = Input.GetAxisRaw("Horizontal");
    float inputY = Input.GetAxisRaw("Vertical");

    Vector2 keyboardDirection = new Vector2(inputX, inputY).normalized;

    if (Input.GetMouseButton(0))
    {
        Vector3 mouseWorldPosition =
            mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 targetPosition = new Vector2(
            mouseWorldPosition.x,
            mouseWorldPosition.y
        );

        Vector2 direction = targetPosition - (Vector2)transform.position;

        
        if (direction.magnitude > 0.1f)
        {
            playerMoveDirection = direction.normalized;
        }
        else
        {
            playerMoveDirection = Vector3.zero;
        }
    }
    else
    {
        playerMoveDirection = keyboardDirection;
    }

    // Animation
    animator.SetFloat("moveX", playerMoveDirection.x);
    animator.SetFloat("moveY", playerMoveDirection.y);
    animator.SetBool("moving", playerMoveDirection != Vector3.zero);


    // Immunity timer
    if (isImmune)
    {
        immunityTimer -= Time.deltaTime;
        blinkTimer -= Time.deltaTime;

        if (blinkTimer <= 0f)
        {
            isVisible = !isVisible;
            playerSprite.enabled = isVisible;

            blinkTimer = blinkInterval;
        }

        if (immunityTimer <= 0f)
        {
            EndImmunity();
        }
    }
   
}


    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(
            playerMoveDirection.x * movespeed,
            playerMoveDirection.y * movespeed
        );
    }

   public void TakeDamage(float damage)
{
    if (!isImmune)
    {
        float reducedDamage = Mathf.Max(
        0,
        damage - Reflect.TotalDamageReduction
        );
        playerHealth -= reducedDamage;
        UIController.Instance.UpdateHealthSlider();

       

        if (playerHealth <= 0)
        {
            
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
            return;
        }

        StartImmunity();
    }

}

    public void GetExperience(int experienceToGet)
    {
        experience += experienceToGet;
        UIController.Instance.UpdateExperienceSlider();
        if(experience >= playerLevels[currentevel - 1])
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        experience -= playerLevels[currentevel - 1];
        currentevel++;
        UIController.Instance.UpdateExperienceSlider();
        UIController.Instance.levelUpButtons[0].ActivateButton(activeAttack);
        UIController.Instance.LevelUpPanelOpen();
    }
        void StartImmunity()
    {
        isImmune = true;
        immunityTimer = immunityDuration;
        blinkTimer = blinkInterval;

        // Visible
        isVisible = true;
        playerSprite.enabled = true;

        // Ignore enemy collisions
        int enemyLayer = LayerMask.NameToLayer("Enemies");

        if (enemyLayer != -1)
        {
            Physics2D.IgnoreLayerCollision(
                gameObject.layer,
                enemyLayer,
                true
            );
        }
    }

    void EndImmunity()
    {
        isImmune = false;
        immunityTimer = 0f;

        // The player is visible again
        isVisible = true;
        playerSprite.enabled = true;

        // Restores enemy collisions
        int enemyLayer = LayerMask.NameToLayer("Enemies");
        if (enemyLayer != -1)
        {
            Physics2D.IgnoreLayerCollision(
                gameObject.layer,
                enemyLayer,
                false
            );
        }
    }
    
}