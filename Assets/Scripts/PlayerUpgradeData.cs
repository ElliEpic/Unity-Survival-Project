
using UnityEngine;

public enum PlayerUpgradeType
{
    PermanentDamageBonus,
    AttackDamageBonus,
    IncreaseMaxHealth,
    HealPlayer,
    IncreaseMoveSpeed,
    ReduceDamageTaken,
    IncreaseExperienceGain
}

[CreateAssetMenu(
    fileName = "NewPlayerUpgrade",
    menuName = "Game/Player Upgrade")]
public class PlayerUpgradeData : ScriptableObject
{
    [Header("Upgrade Information")]
    public string upgradeName;
    [TextArea(2, 4)]
    public string description;
    public Sprite icon;

    [Header("Upgrade Effect")]
    public PlayerUpgradeType upgradeType;
    public int amount = 1;
}
