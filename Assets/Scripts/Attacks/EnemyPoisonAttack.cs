using UnityEngine;

public class EnemyPoisonAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float attackCooldown = 3f;

    private float attackTimer;

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            SpawnProjectile();
            attackTimer = attackCooldown;
        }
    }

    public void SpawnProjectile()
    {
        Vector2 direction = (PlayerMovement.Instance.transform.position - transform.position).normalized;

        GameObject poison = Instantiate(projectile, transform.position, Quaternion.identity);

        PoisonSting poisonSting = poison.GetComponent<PoisonSting>();
        poisonSting.SetDirection(direction);
    }

}