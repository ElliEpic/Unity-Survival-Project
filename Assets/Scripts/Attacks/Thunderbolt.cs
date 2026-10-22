
using System.Collections.Generic;
using UnityEngine;

public class Thunderbolt : Attack
{
    [Header("Lightning")]
    [SerializeField] private GameObject lightningPrefab;
    [SerializeField] private float lightningLifetime = 1.5f;

    [Header("Range Detection")]
    [SerializeField] private CircleCollider2D rangeTrigger;

    [Header("Lightning Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip lightningSound;

    AttackStats stats;

    private readonly List<Enemy> enemiesInRange = new();
    public float spawnCounter = 0;

    

    private void Awake()
    {
        stats = new AttackStats();
        
        

        if (rangeTrigger == null)
            rangeTrigger = GetComponent<CircleCollider2D>();

        if (rangeTrigger == null)
        {
            Debug.LogError(
                "Thunderbolt: No CircleCollider2D assigned or found.",
                this
            );
            enabled = false;
        }



        rangeTrigger.isTrigger = true;
        

        // Wait for the initial cooldown before the first cast.
        
    }


     void Start()
     {
          spawnCounter = stats.cooldown;
          UpdateRange();

     }




     private void Update()
    {



        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0f && enemiesInRange.Count < 0)
        {
            CastLightning();
  
                spawnCounter = Mathf.Max(0.01f, stats.cooldown);
        }

        
    }

    private void UpdateRange()
    {

        rangeTrigger.isTrigger = true;
        rangeTrigger.radius = Mathf.Max(0f, stats.range);
    }

    public override void LevelUp()
    {

        base.LevelUp();

        UpdateRange();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
            Debug.Log($"Thunderbolt: Enemy added: {enemy.name}", this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null)
            enemiesInRange.Remove(enemy);
    }

    public void CastLightning()
    {
 


        // Remove destroyed enemies from the tracked list.
        enemiesInRange.RemoveAll(enemy => enemy == null);

        // Select targets without modifying the tracked list.
        List<Enemy> choices = new(enemiesInRange);
        Debug.Log(choices.Count);
        int strikes = Mathf.Min(
            Mathf.Max(0, base.stats.targetCount),
            choices.Count
        );

        // Don't play audio if no enemies are struck.
        if (strikes == 0)
            return;

        // Play the sound once per cast.
        // This AudioSource must belong to an object that survives
        // longer than the lightning visual effect.
        if (audioSource != null && lightningSound != null)
        {
            audioSource.PlayOneShot(lightningSound);
        }

        for (int i = 0; i < strikes; i++)
        {
            // Choose a random enemy without repeating targets.
            int index = Random.Range(0, choices.Count);
            Enemy target = choices[index];

            choices.RemoveAt(index);

            GameObject lightning = Instantiate(
                lightningPrefab,
                target.transform.position,
                Quaternion.identity
            );

           target.TakeDamage(
            Mathf.RoundToInt(stats.damage) +
            Attack.PermanentDamageBonus
        );

            // Destroy the visual without interrupting the audio.
            Destroy(lightning, lightningLifetime);
        }
    }
}
