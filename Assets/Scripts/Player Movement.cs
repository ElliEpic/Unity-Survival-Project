
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

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

    // Immunity frames after damage
    private bool isImmune;
    [SerializeField] private float immunityDuration;
    [SerializeField] private float immunityTimer;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Collider2D playerCollider;

    public int damageBonus = 3;

    [SerializeField] private float blinkInterval = 0.15f;

    private float blinkTimer;
    private bool isVisible = true;

    public void ApplyPlayerUpgrade(PlayerUpgradeData upgrade)
    {
        if (upgrade == null)
            return;

        switch (upgrade.upgradeType)
        {
            case PlayerUpgradeType.IncreaseMaxHealth:
                playerMaxHealth += upgrade.amount;
                playerHealth += upgrade.amount;
                UIController.Instance.UpdateHealthSlider();
                break;

            case PlayerUpgradeType.HealPlayer:
                playerHealth = Mathf.Min(
                    playerHealth + upgrade.amount,
                    playerMaxHealth
                );
                UIController.Instance.UpdateHealthSlider();
                break;

            case PlayerUpgradeType.IncreaseMoveSpeed:
                movespeed += upgrade.amount;
                break;

            case PlayerUpgradeType.ReduceDamageTaken:
                Reflect.TotalDamageReduction += upgrade.amount;
                break;

            case PlayerUpgradeType.IncreaseExperienceGain:
                Debug.Log("Experience upgrade selected.");
                break;

            case PlayerUpgradeType.PermanentDamageBonus:
                damageBonus += upgrade.amount;
                break;

            case PlayerUpgradeType.AttackDamageBonus:
                if (activeAttack == null ||
                    activeAttack.stats == null)
                    return;

                // foreach (attackStats level in activeAttack.stats)
                // {
                //     if (level != null)
                //         level.damage += upgrade.amount;
                // }
                break;
        }
    }

    public void Start()
    {
        for (int i = playerLevels.Count; i < maxLevel; i++)
        {
            playerLevels.Add(
                Mathf.CeilToInt(
                    playerLevels[playerLevels.Count - 1] * 1.1f + 15
                )
            );
        }

        playerHealth = playerMaxHealth;

        UIController.Instance.UpdateHealthSlider();
        UIController.Instance.UpdateExperienceSlider();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        Vector2 moveInput = Vector2.zero;

        
        if (Gamepad.current != null)
        {
            moveInput = Gamepad.current.leftStick.ReadValue();

            // Controller D-pad also works
            Vector2 dpadInput =
                Gamepad.current.dpad.ReadValue();

            if (dpadInput != Vector2.zero)
            {
                moveInput = dpadInput;
            }
        }

     
        if (Keyboard.current != null)
        {
            float inputX =
                (Keyboard.current.dKey.isPressed ||
                 Keyboard.current.rightArrowKey.isPressed ? 1f : 0f)
                -
                (Keyboard.current.aKey.isPressed ||
                 Keyboard.current.leftArrowKey.isPressed ? 1f : 0f);

            float inputY =
                (Keyboard.current.wKey.isPressed ||
                 Keyboard.current.upArrowKey.isPressed ? 1f : 0f)
                -
                (Keyboard.current.sKey.isPressed ||
                 Keyboard.current.downArrowKey.isPressed ? 1f : 0f);

            Vector2 keyboardInput =
                new Vector2(inputX, inputY);

            if (keyboardInput != Vector2.zero)
            {
                moveInput = keyboardInput.normalized;
            }
        }

        
        if (Mouse.current != null &&
            Mouse.current.leftButton.isPressed &&
            mainCamera != null)
        {
            Vector2 mousePosition =
                mainCamera.ScreenToWorldPoint(
                    Mouse.current.position.ReadValue()
                );

            Vector2 direction =
                mousePosition - (Vector2)transform.position;

            if (direction.magnitude > 0.1f)
            {
                moveInput = direction.normalized;
            }
            else
            {
                moveInput = Vector2.zero;
            }
        }

        // Apply movement direction
        playerMoveDirection = moveInput;

        
        animator.SetFloat("moveX", playerMoveDirection.x);
        animator.SetFloat("moveY", playerMoveDirection.y);

        animator.SetBool(
            "moving",
            playerMoveDirection != Vector3.zero
        );

        
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
        rb.linearVelocity = new Vector2(
            playerMoveDirection.x * movespeed,
            playerMoveDirection.y * movespeed
        );
    }

    public void TakeDamage(float damage)
    {
        if (isImmune)
            return;

        float reducedDamage = Mathf.Max(
            1f,
            damage - Reflect.TotalDamageReduction
        );

        playerHealth -= reducedDamage;

        Debug.Log(
            "Incoming damage: " + damage +
            " | Damage after Reflect: " + reducedDamage
        );

        UIController.Instance.UpdateHealthSlider();

        if (playerHealth <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
            return;
        }

        StartImmunity();
    }

    public void GetExperience(int experienceToGet)
    {
        experience += experienceToGet;

        UIController.Instance.UpdateExperienceSlider();

        if (experience >= playerLevels[currentevel - 1])
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        experience -= playerLevels[currentevel - 1];
        currentevel++;

        UIController.Instance.UpdateExperienceSlider();

        UIController.Instance.levelUpButtons[0]
            .ActivateButton(activeAttack);

        UIController.Instance.LevelUpPanelOpen();
    }

    void StartImmunity()
    {
        isImmune = true;
        immunityTimer = immunityDuration;
        blinkTimer = blinkInterval;

        isVisible = true;
        playerSprite.enabled = true;

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

        isVisible = true;
        playerSprite.enabled = true;

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
