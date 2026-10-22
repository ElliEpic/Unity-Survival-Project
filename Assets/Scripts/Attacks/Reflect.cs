using UnityEngine;

public class Reflect : Attack
{

    [Header("Reflect")]
    [SerializeField] private GameObject reflectPrefab;
    [SerializeField] private float reflectLifetime = 1.5f;
    public static int TotalDamageReduction = 0;

    [Header("Reflect Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip reflectSound;

    public override void LevelUp()
    {
        float oldLevel = attackLevel;

        // Level up Reflect
        base.LevelUp();

        // Only give reduction if the level actually increased
        if (attackLevel > oldLevel)
        {
            TotalDamageReduction++;

            Debug.Log("Reflect selected! Damage reduction: "
                      + TotalDamageReduction);
        }
        UseReflect();
    }
        
    public void UseReflect(){

    // Spawn the effect at the player's position.
        if (reflectPrefab != null)
        {
            Vector3 effectOffset = new Vector3(0f, 0f, 0f);

            GameObject effect = Instantiate(
                reflectPrefab,
                transform.position + effectOffset,
                Quaternion.identity,
                transform
            );
            //effect.transform.SetParent(transform, true);

            // Remove the visual effect after 1.5 seconds.
            Destroy(effect, reflectLifetime);
        }
    }
}