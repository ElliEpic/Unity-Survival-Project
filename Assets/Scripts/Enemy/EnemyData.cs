
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData",
    menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Identity")]
    public string enemyName;
    public Sprite sprite;

    [Header("Combat Stats")]
    public float maxHealth = 10f;
    public float damage = 1f;
    public float moveSpeed = 2f;

    [Header("Rewards")]
    public int experienceToGive = 1;

    [Header("Effects")]
    public float pushTime = 0.2f;
    public GameObject destroyEffect;
}
