
using System.Collections;
using UnityEngine;

public class OranBerrySpawner : MonoBehaviour
{
    public static OranBerrySpawner Instance;

    [SerializeField] private GameObject oranBerryPrefab;
    [SerializeField] private Collider2D spawnArea;
    [SerializeField] private float respawnDelay = 10f;

    private GameObject currentBerry;
    private Coroutine respawnCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        SpawnBerry();
    }

    public void BerryCollected()
    {
        if (respawnCoroutine == null)
        {
            respawnCoroutine = StartCoroutine(RespawnBerry());
        }
    }

    private IEnumerator RespawnBerry()
    {
        yield return new WaitForSeconds(respawnDelay);

        SpawnBerry();

        respawnCoroutine = null;
    }

    private void SpawnBerry()
    {
        if (oranBerryPrefab == null || spawnArea == null)
        {
            Debug.LogWarning(
                "OranBerrySpawner needs a berry prefab and spawn area!"
            );
            return;
        }

        Bounds bounds = spawnArea.bounds;

        Vector3 randomPosition = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0f
        );

        currentBerry = Instantiate(
            oranBerryPrefab,
            randomPosition,
            Quaternion.identity
        );
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
