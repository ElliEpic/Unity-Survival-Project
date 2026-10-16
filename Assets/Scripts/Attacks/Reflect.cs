using UnityEngine;

public class Reflect : Attack
{
    [Header("Defense Boost")]
    [SerializeField] private GameObject reflectPrefab;
    [SerializeField] private float reflectLifetime = 1.5f;
    [SerializeField] private Transform player;

    // Total damage reduction from all Defense Boost upgrades.
    public static int TotalDamageReduction { get; private set; } = 0;

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

    public override void LevelUp()
    {
        int previousLevel = attackLevel;

        // Level up the Defense Boost.
        base.LevelUp();

        // Don't apply a bonus if the level didn't change.
        if (attackLevel == previousLevel)
            return;

        attackStats current = CurrentStats;

        if (current == null)
            return;

        // Add this upgrade's defense value.
        int defenseAmount = Mathf.RoundToInt(current.damageBuff);

        TotalDamageReduction += Mathf.Max(0, defenseAmount);

        // Show the visual effect.
        ShowDefenseEffect();
    }

    private void ShowDefenseEffect()
    {
        if (reflectPrefab == null)
            return;

        Transform target = player != null ? player : transform;

        GameObject effect = Instantiate(
            reflectPrefab,
            target.position + Vector3.up * 1.5f,
            Quaternion.identity,
            target
        );

        Destroy(effect, reflectLifetime);
    }
}