
using System.Collections.Generic;
using UnityEngine;

public class Thunderbolt : Attack
{
    [Header("Thunderbolt Settings")]
    [SerializeField] private GameObject lightningPrefab;
    [SerializeField] private Camera mainCamera;

    private float cooldownTimer;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Wait for the cooldown before the first strike.
        if (stats != null && stats.Count > 0)
        {
            cooldownTimer = stats[attackLevel].cooldown;
        }
    }

    private void Update()
    {
        if (stats == null || stats.Count == 0)
            return;

        if (attackLevel < 0 || attackLevel >= stats.Count)
            return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            CastThunderbolt();
            cooldownTimer = stats[attackLevel].cooldown;
        }
    }

    private void CastThunderbolt()
    {
        if (mainCamera == null || lightningPrefab == null)
            return;

        // Find all enemies currently in the scene.
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        List<Enemy> targets = new List<Enemy>();

        // Keep only enemies visible inside the camera.
        foreach (Enemy enemy in allEnemies)
        {
            if (enemy == null)
                continue;

            Vector3 screenPosition =
                mainCamera.WorldToViewportPoint(
                    enemy.transform.position
                );

            bool isVisible =
                screenPosition.z > 0f &&
                screenPosition.x >= 0f &&
                screenPosition.x <= 1f &&
                screenPosition.y >= 0f &&
                screenPosition.y <= 1f;

            if (isVisible)
            {
                targets.Add(enemy);
            }
        }

        int strikeCount = Mathf.Min(
            stats[attackLevel].targetCount,
            targets.Count
        );

        for (int i = 0; i < strikeCount; i++)
        {
            int randomIndex = Random.Range(0, targets.Count);
            Enemy target = targets[randomIndex];

            // Prevent the same enemy being selected twice.
            targets.RemoveAt(randomIndex);

            Instantiate(
                lightningPrefab,
                target.transform.position,
                Quaternion.identity
            );

            target.TakeDamage(stats[attackLevel].damage);
        }
    }
}
