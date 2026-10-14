
using System.Collections.Generic;
using UnityEngine;

public class Thunderbolt : Attack
{
    [Header("Lightning")]
    [SerializeField] private GameObject lightningPrefab;

    [Header("Range Detection")]
    [SerializeField] private CircleCollider2D rangeTrigger;

    [SerializeField] private float lightningLifetime = 1.5f;

    private readonly List<Enemy> enemiesInRange = new();
    private float spawnCounter;

    private attackStats CurrentStats
    {
        get
        {
            if (stats == null ||
                attackLevel < 0 ||
                attackLevel >= stats.Count)
            {
                return null;
            }

            return stats[attackLevel];
        }
    }

    private void Awake()
    {
        if (rangeTrigger == null)
            rangeTrigger = GetComponent<CircleCollider2D>();

        if (rangeTrigger == null)
        {
            enabled = false;
            return;
        }

        if (CurrentStats == null)
        {
            enabled = false;
            return;
        }

        rangeTrigger.isTrigger = true;
        UpdateRange();

        // Wait for the initial cooldown before the first cast.
        spawnCounter = Mathf.Max(0f, CurrentStats.cooldown);

    }

    private void Update()
    {
        attackStats current = CurrentStats;

        if (current == null)
        {
            return;
        }

        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0f)
        {

            CastLightning();

            // Read the current cooldown after casting.
            current = CurrentStats;

            if (current != null)
                spawnCounter = Mathf.Max(0.01f, current.cooldown);
        }
    }

    private void UpdateRange()
    {
        attackStats current = CurrentStats;

        if (rangeTrigger == null || current == null)
            return;

        rangeTrigger.isTrigger = true;
        rangeTrigger.radius = Mathf.Max(0f, current.range);

    }

    public override void LevelUp()
    {
        int previousLevel = attackLevel;

        base.LevelUp();

        if (attackLevel == previousLevel)
        {


            return;
        }

        UpdateRange();

    }

 private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log(
        $"[Thunderbolt] Trigger detected: {other.name}, " +
        $"Layer: {LayerMask.LayerToName(other.gameObject.layer)}",
        this
    );

    Enemy enemy = other.GetComponentInParent<Enemy>();

    if (enemy != null && !enemiesInRange.Contains(enemy))
    {
        enemiesInRange.Add(enemy);
        Debug.Log($"[Thunderbolt] Enemy added: {enemy.name}", this);
    }
    else if (enemy == null)
    {
        Debug.Log($"[Thunderbolt] {other.name} has no Enemy component in its parent hierarchy.",this);
    }
}

    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
    }

    public void CastLightning()
    {
        attackStats current = CurrentStats;

        if (lightningPrefab == null)
        {
            return;
        }

        // Remove destroyed enemies from the tracked list.
        enemiesInRange.RemoveAll(enemy => enemy == null);

        // Create a separate list so the tracked list remains intact.
        List<Enemy> choices = new(enemiesInRange);

        int strikes = Mathf.Min(
            Mathf.Max(0, current.targetCount),
            choices.Count
        );

        if (strikes == 0)
        {
            return;
        }

        for (int i = 0; i < strikes; i++)
        {
            // Select a random enemy without repeating targets this cast.
            int index = Random.Range(0, choices.Count);
            Enemy target = choices[index];

            choices.RemoveAt(index);

            GameObject lightning = Instantiate(
                lightningPrefab,
                target.transform.position,
                Quaternion.identity
            );

            target.TakeDamage(Mathf.RoundToInt(current.damage));

            Destroy(lightning, lightningLifetime);
        }
    }
}
