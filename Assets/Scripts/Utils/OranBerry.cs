
using UnityEngine;

public class OranBerry : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;
        if (collected)
            return;
             Debug.Log("Berry touched: " + other.name);

        PlayerMovement player =
            other.GetComponentInParent<PlayerMovement>();

        if (player == null)
            return;

        collected = true;

        // Heal 20% of maximum health
        float healAmount = player.playerMaxHealth * 0.2f;

        player.playerHealth = Mathf.Min(
            player.playerHealth + healAmount,
            player.playerMaxHealth
        );

        // Refresh the health bar
        UIController.Instance.UpdateHealthSlider();

        // Tell the spawner to create another berry later
        if (OranBerrySpawner.Instance != null)
        {
            OranBerrySpawner.Instance.BerryCollected();
        }

        // Remove this berry
        Destroy(gameObject);
    }
}
